using System.Collections.Generic;

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

        public void OnClicked(Vector3Data clickPos)
        {
            Cell cell = FindClosestCell(clickPos);
            HighlightCell(cell);
        }

        private Cell FindClosestCell(Vector3Data position)
        {
            Cell cell = new Cell();
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

        private static void HighlightCell(Cell cell)
        {
            cell.Color = new ColorData(1, 0, 0, 0);
        }
    }
}