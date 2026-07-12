using System;
using System.Collections.Generic;
using Core.Structs;

namespace Core
{
    public class Cell
    {
        //Messy
        public readonly Vector3Data Center;
        private ColorData? _color;
        public ColorData Color
        {
            get => _color ?? new ColorData(0, 0, 0, 1);
            private set => _color = value;
        }

        public bool IsWater {get; internal set; }
        public int? OccupiedBy { get; internal set; }
        public List<Vector3Data> Corners { get; }
        public List<Cell> Neighbors { get; } = new List<Cell>();
        
        public List<Sector> Sectors { get; } = new List<Sector>();

        internal Cell(Vector3Data center, List<Vector3Data> corners)
        {
            Center = center;
            Corners = corners;
            for (int c = 1; c < corners.Count; c++)
            {
                Sectors.Add(new Sector(this, corners[c - 1], corners[c]));
            }
            Sectors.Add(new Sector(this, corners[Corners.Count - 1], corners[0]));
        }
        public void SetColor(ColorData color)
        {
            if (_color != null) throw new InvalidOperationException();
            Color = color;
            foreach (var s in Sectors)
            {
                s.Color = color * 0.9f;
            }
        }

        public void Highlight()
        {
            _color = new ColorData(1, 0, 0, 0);
        }

        public class Sector
        {
            public Cell Parent { get; private set; }
            public ColorData Color { get; internal set; } = new ColorData(0, 0, 0, 1);
            public Vector3Data Center {get; private set; }

            public Sector(Cell parent, Vector3Data p1, Vector3Data p2)
            {
                Parent =  parent;
                Center = (parent.Center + p1 + p2) / 3;
            }

            public void Highlight()
            {
                Color = new ColorData(1, 0, 0, 0);
            }
        }
        
        public Sector GetClosestSector(Vector3Data point)
        {
            Sector sector = null;
            float closest = float.MaxValue;

            foreach (Sector s in Sectors)
            {
                float sqrDist = (s.Center - point).SqrMagnitude;
                if (sqrDist < closest)
                {
                    sector = s;
                    closest = sqrDist;
                }
            }
            return sector;
        }
    }
}