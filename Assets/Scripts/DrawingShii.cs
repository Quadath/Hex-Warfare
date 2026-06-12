using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class DrawingShii : MonoBehaviour
{
    [SerializeField] private float _size = 1f;

    private MeshFilter _meshFilter;
    private MeshRenderer _renderer;

    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();

        _meshFilter.mesh = CreateCubeMesh(_size);

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

    private static Mesh CreateCubeMesh(float size)
    {
        float half = size * 0.5f;

        Vector3[] faceNormals =
        {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right,
            Vector3.up,
            Vector3.down
        };

        Vector3[] faceUps =
        {
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.back,
            Vector3.forward
        };

        var vertices = new List<Vector3>();
        var triangles = new List<int>();

        for (int face = 0; face < faceNormals.Length; face++)
        {
            Vector3 normal = faceNormals[face];
            Vector3 up = faceUps[face];
            Vector3 right = Vector3.Cross(up, normal);

            int startIndex = vertices.Count;

            vertices.Add((normal - right - up) * half);
            vertices.Add((normal + right - up) * half);
            vertices.Add((normal + right + up) * half);
            vertices.Add((normal - right + up) * half);

            triangles.Add(startIndex);
            triangles.Add(startIndex + 1);
            triangles.Add(startIndex + 2);

            triangles.Add(startIndex);
            triangles.Add(startIndex + 2);
            triangles.Add(startIndex + 3);
        }

        Mesh mesh = new()
        {
            name = "Generated Cube",
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray()
        };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}
