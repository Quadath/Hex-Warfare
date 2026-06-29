using System.Collections.Generic;
using Shared;

namespace Core
{
    public class Cell
    {
        public Vector3Data Center;
        public ColorData Color;

        public bool IsWater;
        public List<Vector3Data> Corners = new List<Vector3Data>();
        public List<Cell> Neighbors = new List<Cell>();
        
        public List<Sector> Sectors { get; private set; } = new List<Sector>();

        public void Init()
        {
            for (int i = 0; i < Corners.Count; i++)
            {
                //Sectors.Add();
            }
        }

        public Sector GetClosestSector(Vector3Data point)
        {
            Sector sector = new Sector();
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

        public class Sector
        {
            public ColorData Color;
            public Vector3Data Center;

            public Sector()
            {
                
            }

            public Sector(ColorData color, Vector3Data center)
            {
                Color = color;
                Center = center;
            }

            public void Highlight()
            {
                Color = new ColorData(1, 0, 0, 0);
            }
        }
    }
}