using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Icosphere : MonoBehaviour
{
public int subdivisions = 2;
static float radius = 1f;

void Start()
{
    var (vertices, triangles) = CreateIcosphere(subdivisions);

    var grid = new DualGrid();
    var cells = grid.Generate(vertices, triangles);

    Mesh mesh = BuildMesh(cells);

    GetComponent<MeshFilter>().mesh = mesh;
}

// =========================
// ICOSPHERE
// =========================

(Vector3[], int[]) CreateIcosphere(int subdivisions)
{
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();

    float t = (1f + Mathf.Sqrt(5f)) / 2f;

    vertices.Add(new Vector3(-1, t, 0).normalized);
    vertices.Add(new Vector3(1, t, 0).normalized);
    vertices.Add(new Vector3(-1, -t, 0).normalized);
    vertices.Add(new Vector3(1, -t, 0).normalized);

    vertices.Add(new Vector3(0, -1, t).normalized);
    vertices.Add(new Vector3(0, 1, t).normalized);
    vertices.Add(new Vector3(0, -1, -t).normalized);
    vertices.Add(new Vector3(0, 1, -t).normalized);

    vertices.Add(new Vector3(t, 0, -1).normalized);
    vertices.Add(new Vector3(t, 0, 1).normalized);
    vertices.Add(new Vector3(-t, 0, -1).normalized);
    vertices.Add(new Vector3(-t, 0, 1).normalized);

    triangles.AddRange(new int[]
    {
        0,11,5, 0,5,1, 0,1,7, 0,7,10, 0,10,11,
        1,5,9, 5,11,4, 11,10,2, 10,7,6, 7,1,8,
        3,9,4, 3,4,2, 3,2,6, 3,6,8, 3,8,9,
        4,9,5, 2,4,11, 6,2,10, 8,6,7, 9,8,1
    });

    Dictionary<long, int> midpointCache = new Dictionary<long, int>();

    int GetMidpoint(int a, int b)
    {
        long key = ((long)Mathf.Min(a, b) << 32) + Mathf.Max(a, b);

        if (midpointCache.TryGetValue(key, out int index))
            return index;

        Vector3 mid = ((vertices[a] + vertices[b]) * 0.5f).normalized;

        index = vertices.Count;
        vertices.Add(mid);
        midpointCache[key] = index;

        return index;
    }

    for (int i = 0; i < subdivisions; i++)
    {
        List<int> newTris = new List<int>();

        for (int j = 0; j < triangles.Count; j += 3)
        {
            int a = triangles[j];
            int b = triangles[j + 1];
            int c = triangles[j + 2];

            int ab = GetMidpoint(a, b);
            int bc = GetMidpoint(b, c);
            int ca = GetMidpoint(c, a);

            newTris.AddRange(new int[]
            {
                a, ab, ca,
                b, bc, ab,
                c, ca, bc,
                ab, bc, ca
            });
        }

        triangles = newTris;
    }

    for (int i = 0; i < vertices.Count; i++)
        vertices[i] = vertices[i].normalized * radius;

    return (vertices.ToArray(), triangles.ToArray());
}

// =========================
// DUAL GRAPH
// =========================

class DualGrid
{
    public class Triangle
    {
        public int a, b, c;
        public Vector3 center;
    }

    public class Cell
    {
        public Vector3 center;
        public List<Vector3> corners = new List<Vector3>();
        public List<int> neighbors = new List<int>();
    }

    public List<Cell> Generate(Vector3[] vertices, int[] triangles)
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
            //cell.center = vertices[v] - vertices[v] * 0.01f; //pull inside
            cell.center = vertices[v];

            foreach (var ti in vToT[v])
                cell.corners.Add(tris[ti].center * radius);
            var center = new Vector3();
            foreach (var c in cell.corners)
            {
                center += c;
            }

            center /= cell.corners.Count;
            cell.center = center;
            SortCorners(cell.center, cell.corners);

            cells.Add(cell);
        }

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

        return cells;
    }

    void TryAdd(List<Cell> cells, int self, int other)
    {
        if (self != other && !cells[self].neighbors.Contains(other))
            cells[self].neighbors.Add(other);
    }

    void SortCorners(Vector3 center, List<Vector3> corners)
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
    }
}

// =========================
// MESH
// =========================

Mesh BuildMesh(List<DualGrid.Cell> cells)
{
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();

    foreach (var cell in cells)
    {
        int centerIndex = vertices.Count;
        vertices.Add(cell.center);

        for (int i = 0; i < cell.corners.Count; i++)
        {
            int next = (i + 1) % cell.corners.Count;

            vertices.Add(cell.corners[i]);
            vertices.Add(cell.corners[next]);

            int a = centerIndex;
            int b = vertices.Count - 2;
            int c = vertices.Count - 1;

            triangles.Add(a);
            triangles.Add(b);
            triangles.Add(c);
        }
    }

    Mesh mesh = new Mesh();
    mesh.vertices = vertices.ToArray();
    mesh.triangles = triangles.ToArray();
    mesh.RecalculateNormals();

    return mesh;
}

}
