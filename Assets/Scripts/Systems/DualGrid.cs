using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core;

namespace Systems
{
    class DualGrid
        {
            public class Triangle
            {
                public int a, b, c;
                public Vector3 center;
            }

            public static List<Cell> Generate(Vector3[] vertices, int[] triangles, float radius)
            {
                List<Triangle> tris = new List<Triangle>();

                for (int i = 0; i < triangles.Length; i += 3)
                {
                    Triangle t = new Triangle();
                    t.a = triangles[i];
                    t.b = triangles[i + 1];
                    t.c = triangles[i + 2];

                    t.center = ((vertices[t.a] + vertices[t.b] + vertices[t.c]) / 3f).normalized;

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
                    cell.center = vertices[v];
                    cell._center = Vector3Extensions.ToCore(vertices[v]);

                    foreach (var ti in vToT[v])
                    {
                        cell.corners.Add(tris[ti].center * radius);
                        cell._corners.Add(Vector3Extensions.ToCore(tris[ti].center * radius));
                    }
                    
                    //move centers of cells to make hexagons 
                    var center = new Vector3Data();
                    foreach (var c in cell._corners)
                    {
                        center += c;
                    }
                    center /= cell.corners.Count;
                    cell.center = Vector3Extensions.ToUnity(center);
                    cell._center = center;

                    cell._corners = SortCorners(Vector3Extensions.ToUnity(cell._center), cell._corners.ToUnity())
                        .ToCore(); //breaks here
                    //cell.corners = cell._corners.ToUnity();
                    

                    var col =  new Color(.25f, .2f, .2f);
                    cell.color = new ColorData(col.r, col.g, col.b, 1);

                    cells.Add(cell);
                }
                
                //Assigning heighbors to cells
                for (int i = 0; i < cells.Count; i++)
                {
                    foreach (var ti in vToT[i])
                    {
                        var t = tris[ti];
                        TryAdd(cells, i, t.a);
                        TryAdd(cells, i, t.b);
                        TryAdd(cells, i, t.c);
                    }
                }
                
                foreach (var cell in  cells)
                {
                    var threshold = .99f;
                    var water = Random.value >= threshold;
                    if (water)
                    {
                        cell.isWater = true;
                        var col =  new Color(.51f, .72f, .94f);
                        cell.color = new ColorData(col.r, col.g, col.b, 1);
                    }
                }
                foreach (var cell in  cells)
                {
                    var isWaterInNeighborhood = false;
                    foreach (var n in cell.neighbors)
                    {
                        if (!cells[n].isWater) continue;
                        isWaterInNeighborhood = true;
                        Debug.Log("Water!!");
                    }
                    if  (!isWaterInNeighborhood) continue;

                    var threshold = .2f;
                    var water = Random.value >= threshold;
                    if (water)
                    {
                        cell.isWater = true;
                        var col =  new Color(.51f, .72f, .94f);
                        cell.color = new ColorData(col.r, col.g, col.b, 1);
                    }
                }

                return cells;
            }

            static void TryAdd(List<Cell> cells, int self, int other)
            {
                if (self != other && !cells[self].neighbors.Contains(other))
                    cells[self].neighbors.Add(other);
            }

            static List<Vector3> SortCorners(Vector3 center, List<Vector3> corners)
            {
                Vector3 normal = center.normalized;

                Vector3 axisX = Vector3.Cross(normal, Vector3.up);
                if (axisX.sqrMagnitude < 0.001f)
                    axisX = Vector3.Cross(normal, Vector3.right);

                axisX.Normalize();
                Vector3 axisY = Vector3.Cross(normal, axisX);

                corners.Sort((a, b) =>
                {
                    Vector3 da = (a - center).normalized;
                    Vector3 db = (b - center).normalized;

                    float angleA = Mathf.Atan2(Vector3.Dot(da, axisY), Vector3.Dot(da, axisX));
                    float angleB = Mathf.Atan2(Vector3.Dot(db, axisY), Vector3.Dot(db, axisX));

                    return angleA.CompareTo(angleB);
                });
                return corners;
            }
        }
}