using System;
using System.Collections.Generic;

namespace Core
{
    public static class Icosphere
    {
        public static (Vector3Data[], int[]) CreateIcosphere(int subdivisions, float radius)
        {
            List<Vector3Data> vertices = new List<Vector3Data>();
            List<int> triangles = new List<int>();
            

            float t = (1f + (float)Math.Sqrt(5f)) / 2f;

            vertices.Add(new Vector3Data(-1, t, 0).Normalized);
            vertices.Add(new Vector3Data(1, t, 0).Normalized);
            vertices.Add(new Vector3Data(-1, -t, 0).Normalized);
            vertices.Add(new Vector3Data(1, -t, 0).Normalized);

            vertices.Add(new Vector3Data(0, -1, t).Normalized);
            vertices.Add(new Vector3Data(0, 1, t).Normalized);
            vertices.Add(new Vector3Data(0, -1, -t).Normalized);
            vertices.Add(new Vector3Data(0, 1, -t).Normalized);

            vertices.Add(new Vector3Data(t, 0, -1).Normalized);
            vertices.Add(new Vector3Data(t, 0, 1).Normalized);
            vertices.Add(new Vector3Data(-t, 0, -1).Normalized);
            vertices.Add(new Vector3Data(-t, 0, 1).Normalized);

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
                long key = ((long)Math.Min(a, b) << 32) + Math.Max(a, b);

                if (midpointCache.TryGetValue(key, out int index))
                    return index;

                Vector3Data mid = ((vertices[a] + vertices[b]) * 0.5f).Normalized;

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
            {
                vertices[i] = vertices[i].Normalized * radius;
            }

            return (vertices.ToArray(), triangles.ToArray());
        }
    }
}
