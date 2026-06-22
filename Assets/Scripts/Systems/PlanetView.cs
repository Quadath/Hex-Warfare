using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Systems
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class PlanetView : MonoBehaviour
    {
        public int subdivisions = 2;
        public float radius = 1f;

        private Planet _planet = new Planet();

        void Start()
        {
            _planet.Generate(subdivisions, radius);
            var mesh = BuildMesh(_planet.Cells);
            GetComponent<MeshFilter>().mesh = mesh;
        }
        
        private static Mesh BuildMesh(List<Cell> cells)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();
            List<Color> colors = new List<Color>();

            foreach (var cell in cells)
            {
                int centerIndex = vertices.Count;
                vertices.Add(Vector3Extensions.ToUnity(cell.Center));
                colors.Add(new Color(cell.Color.R, cell.Color.G, cell.Color.B, cell.Color.A)); //fix
                

                for (int i = 0; i < cell.Corners.Count; i++)
                {
                    int next = (i + 1) % cell.Corners.Count;

                    vertices.Add(Vector3Extensions.ToUnity(cell.Corners[i]));
                    colors.Add(new Color(cell.Color.R, cell.Color.G, cell.Color.B, cell.Color.A)); //fix 
                    
                    vertices.Add(Vector3Extensions.ToUnity(cell.Corners[next]));
                    colors.Add(new Color(cell.Color.R, cell.Color.G, cell.Color.B, cell.Color.A)); //fix
                    
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
            mesh.colors = colors.ToArray();
            mesh.RecalculateNormals();

            return mesh;
        }
    } }

