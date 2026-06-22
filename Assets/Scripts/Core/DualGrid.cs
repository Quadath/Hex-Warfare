using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace Core
{
    public class DualGrid
    {
        private class Triangle
        {
            public int a, b, c;
            public Vector3Data center;
        }

        public static List<Cell> Generate(Vector3Data[] vertices, int[] triangles, float radius)
        {
            List<Triangle> tris = new List<Triangle>();

            for (int i = 0; i < triangles.Length; i += 3)
            {
                Triangle t = new Triangle();
                t.a = triangles[i];
                t.b = triangles[i + 1];
                t.c = triangles[i + 2];

                t.center = ((vertices[t.a] + vertices[t.b] + vertices[t.c]) / 3f).Normalized;

                tris.Add(t);
            }

            Dictionary<int, List<int>> vToT = new Dictionary<int, List<int>>();

            for (int i = 0; i < vertices.Length; i++)
                vToT[i] = new List<int>();

            for (int i = 0; i < tris.Count; i++)
            {
                vToT[tris[i].a].Add(i);
                vToT[tris[i].b].Add(i);
                vToT[tris[i].c].Add(i);
            }

            List<Cell> cells = new List<Cell>();

            for (int v = 0; v < vertices.Length; v++)
            {
                Cell cell = new Cell();
                cell.Center = vertices[v];

                foreach (var ti in vToT[v])
                {
                    cell.Corners.Add(tris[ti].center * radius);
                }
                
                //move centers of cells to make hexagons (pull center inside)
                var center = new Vector3Data();
                foreach (var c in cell.Corners)
                {
                    center += c;
                }
                center /= cell.Corners.Count;
                cell.Center = center;

                cell.Corners = SortCorners(cell.Center, cell.Corners); //breaks here
                cells.Add(cell);
            }
            
            //Assigning heighbors to cells
            for (int i = 0; i < cells.Count; i++)
            {
                foreach (var ti in vToT[i])
                {
                    var t = tris[ti];
                    TryAdd(cells, cells[i], cells[t.a]);
                    TryAdd(cells, cells[i], cells[t.b]);
                    TryAdd(cells, cells[i], cells[t.c]);
                }
            }

            return cells;
        }

        private static void TryAdd(List<Cell> cells, Cell self, Cell other)
        {
            if (self != other && !self.Neighbors.Contains(other))
                self.Neighbors.Add(other);
        }

        private static List<Vector3Data> SortCorners(Vector3Data center, List<Vector3Data> corners)
        {
            Vector3Data normal = center.Normalized;

            Vector3Data axisX = Vector3Data.Cross(normal, Vector3Data.Up);
            if (axisX.SqrMagnitude < 0.001f)
                axisX = Vector3Data.Cross(normal, Vector3Data.Right);

            axisX.Normalize();
            Vector3Data axisY = Vector3Data.Cross(normal, axisX);

            corners.Sort((a, b) =>
            {
                Vector3Data da = (a - center).Normalized;
                Vector3Data db = (b - center).Normalized;

                float angleA = (float)Math.Atan2(Vector3Data.Dot(da, axisY), Vector3Data.Dot(da, axisX));
                float angleB = (float)Math.Atan2(Vector3Data.Dot(db, axisY), Vector3Data.Dot(db, axisX));

                return angleA.CompareTo(angleB);
            });
            return corners;
        }

        public static void GenerateWater(List<Cell> cells)
        {
            Random rnd = new Random();
            var col =  new ColorData(.25f, .2f, .2f, 1);
            foreach (var cell in  cells)
            {
                cell.Color = col;
                var threshold = 99;
                var water = rnd.Next(0, 100) >= threshold;
                if (!water) continue;
                cell.IsWater = true;
                cell.Color =  new ColorData(.96f, .45f, .18f, 1);
            }
            foreach (var cell in  cells)
            {
                var isWaterInNeighborhood = false;
                foreach (var c in cell.Neighbors)
                {
                    if (!c.IsWater) continue;
                    isWaterInNeighborhood = true;
                }
                if  (!isWaterInNeighborhood) continue;

                var threshold = 20;
                var water = rnd.Next(0, 100) >= threshold;
                if (!water) continue;
                cell.IsWater = true;
                col =  new ColorData(.96f, .45f, .18f, 1);
                cell.Color = col;
                
            }
        }
    }
}