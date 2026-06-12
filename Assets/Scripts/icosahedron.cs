using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class icosahedron : MonoBehaviour
{
    [SerializeField] private float _size = 1f;
    [SerializeField] private bool _flatShading = true;

    private MeshFilter _meshFilter;
    private MeshRenderer _renderer;

    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();

        _meshFilter.mesh = CreateMesh(_size, _flatShading);

        if (_renderer.sharedMaterial == null)
        {
            Shader shader = Shader.Find("Universal Render Pipeline/Lit");

            if (shader == null)
            {
                shader = Shader.Find("Standard");
            }

            if (shader != null)
            {
                _renderer.sharedMaterial = new Material(shader);
            }
        }
    }

    private static Mesh CreateMesh(float size, bool flatShading)
    {
        float radius = size * 0.5f;
        float phi = (1f + Mathf.Sqrt(5f)) * 0.5f;

        Vector3[] baseVertices =
        {
            new(-1f, phi, 0f),
            new(1f, phi, 0f),
            new(-1f, -phi, 0f),
            new(1f, -phi, 0f),

            new(0f, -1f, phi),
            new(0f, 1f, phi),
            new(0f, -1f, -phi),
            new(0f, 1f, -phi),

            new(phi, 0f, -1f),
            new(phi, 0f, 1f),
            new(-phi, 0f, -1f),
            new(-phi, 0f, 1f),
        };

        for (int i = 0; i < baseVertices.Length; i++)
        {
            baseVertices[i] = baseVertices[i].normalized * radius;
        }

        var vertices = flatShading ? new List<Vector3>() : new List<Vector3>(baseVertices);
        var triangles = GenerateConvexHullTriangles(baseVertices, flatShading, vertices);

        Mesh mesh = new()
        {
            name = "Generated Icosahedron",
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray()
        };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }

    private static List<int> GenerateConvexHullTriangles(
        Vector3[] sourceVertices,
        bool flatShading,
        List<Vector3> meshVertices)
    {
        const float epsilon = 0.0001f;

        var triangles = new List<int>();

        for (int a = 0; a < sourceVertices.Length - 2; a++)
        {
            for (int b = a + 1; b < sourceVertices.Length - 1; b++)
            {
                for (int c = b + 1; c < sourceVertices.Length; c++)
                {
                    int ia = a;
                    int ib = b;
                    int ic = c;

                    Vector3 va = sourceVertices[ia];
                    Vector3 vb = sourceVertices[ib];
                    Vector3 vc = sourceVertices[ic];
                    Vector3 normal = Vector3.Cross(vb - va, vc - va);

                    if (normal.sqrMagnitude < epsilon)
                    {
                        continue;
                    }

                    if (!IsHullFace(sourceVertices, ia, ib, ic, normal, epsilon))
                    {
                        continue;
                    }

                    Vector3 faceCenter = (va + vb + vc) / 3f;

                    if (Vector3.Dot(normal, faceCenter) < 0f)
                    {
                        (ib, ic) = (ic, ib);
                        (vb, vc) = (vc, vb);
                    }

                    if (flatShading)
                    {
                        int startIndex = meshVertices.Count;

                        meshVertices.Add(va);
                        meshVertices.Add(vb);
                        meshVertices.Add(vc);

                        triangles.Add(startIndex);
                        triangles.Add(startIndex + 1);
                        triangles.Add(startIndex + 2);
                    }
                    else
                    {
                        triangles.Add(ia);
                        triangles.Add(ib);
                        triangles.Add(ic);
                    }
                }
            }
        }

        return triangles;
    }

    private static bool IsHullFace(
        Vector3[] vertices,
        int a,
        int b,
        int c,
        Vector3 normal,
        float epsilon)
    {
        bool hasPointInFront = false;
        bool hasPointBehind = false;
        Vector3 planePoint = vertices[a];

        for (int i = 0; i < vertices.Length; i++)
        {
            if (i == a || i == b || i == c)
            {
                continue;
            }

            float distance = Vector3.Dot(normal, vertices[i] - planePoint);

            if (distance > epsilon)
            {
                hasPointInFront = true;
            }
            else if (distance < -epsilon)
            {
                hasPointBehind = true;
            }

            if (hasPointInFront && hasPointBehind)
            {
                return false;
            }
        }

        return true;
    }
}
