using System;
using System.Collections.Generic;
using Core.Structs;

namespace Core
{
    public class Cell
    {
        //Messy
        public readonly Vector3Data Center;
        public List<Vector3Data> Corners { get; }
        public List<Cell> Neighbors { get; } = new List<Cell>();
        public List<Sector> Sectors { get; } = new List<Sector>();
        public Entity Building { get; private set; }
        

        public int OccupiedBy { get; internal set; }
        public bool IsWater {get; internal set; }
        //DEBUG
        private bool isHighlighted;
        
        public ColorData Color
        {
            get
            {
                if (isHighlighted) return Constants.HighlightedColor;
                var substanceCol = IsWater ? Constants.WaterColor : Constants.GroundColor;
                if (OccupiedBy == 0) return substanceCol;
                var playerCol = Constants.PlayerColors[OccupiedBy];
                return ColorData.Lerp(substanceCol, playerCol, .15f);
            }
        }

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
        
        internal void Occupy(int player) => OccupiedBy = player;
        // public void SetColor(ColorData color)
        // {
        //     if (_color != null) throw new InvalidOperationException();
        //     Color = color;
        //     foreach (var s in Sectors)
        //     {
        //         s.Color = color * 0.9f;
        //     }
        // }

        public void Highlight() => isHighlighted = true;

        public class Sector
        {
            public Cell Parent { get; private set; }

            public ColorData Color => Parent.Color * .9f;

            public Vector3Data Center {get; private set; }

            public Sector(Cell parent, Vector3Data p1, Vector3Data p2)
            {
                Parent =  parent;
                Center = (parent.Center + p1 + p2) / 3;
            }

            public void Highlight()
            {
               // Color = Constants.HighlightedColor;
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