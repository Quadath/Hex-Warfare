using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Shared;

namespace Core
{
    public class Planet
    {
        public List<Cell> Cells { get; private set; }

        public void Generate(int subdivisions, float radius)
        {
            var (vertices, triangles) = Icosphere.CreateIcosphere(subdivisions, radius);
            Cells = DualGrid.Generate(vertices, triangles, radius);
            DualGrid.GenerateWater(Cells);
        }
        
        public Cell FindClosestCell(Vector3Data position)
        {
            Cell cell = null;
            float closest = float.MaxValue;

            foreach (Cell c in Cells)
            {
                float sqrDist = (c.Center - position).SqrMagnitude;
                if (sqrDist < closest)
                {
                    cell = c;
                    closest = sqrDist;
                }
            }
            return cell;
        }
        
        public static void HighlightCell(Cell cell)
        {
            cell.Highlight();
        }
    }
}