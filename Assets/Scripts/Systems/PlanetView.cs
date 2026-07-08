using System;
using System.Collections.Generic;
using Shared;
using Core;
using Utils;
using UnityEngine;

namespace Systems
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class PlanetView : UnitySystem
    {
        public int subdivisions = 2;
        private float radius = 1f;

        private Planet _planet;

        private Cell _startCell;
        private Cell _endCell;

        public override void Init(Game game)
        {
            _planet = game.WorldCommands.GeneratePlanet(subdivisions, radius);
            Draw();
        }
        public void Draw()
        {
            var mesh = BuildMesh(_planet.Cells);
            GetComponent<MeshFilter>().mesh = mesh;
            GetComponent<MeshCollider>().sharedMesh = mesh;
        }

        public Cell OnClicked(Vector3 clickPos)
        {
            Cell c = _planet.FindClosestCell(Vector3Extensions.ToCore(clickPos));
            //List<Cell> path = (_startCell != null && _endCell != null) ? Planet.FindPath(_startCell, _endCell, radius) : null;
            // if (path is { Count: > 0 })
            // {
            //     foreach(Cell cell in path)
            //     {
            //         Planet.HighlightCell(cell);
            //     }
            // }
            //Draw();
            return c;
        }
        
        //private static Color[] _colors = new Color[6] { Color.red, Color.green, Color.blue, Color.yellow, Color.purple, Color.aquamarine };
        
        private static Mesh BuildMesh(List<Cell> cells)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();
            List<Color> colors = new List<Color>();

            foreach (var cell in cells)
            {
                int centerIndex = vertices.Count;
                vertices.Add(Vector3Extensions.ToUnity(cell.Center));
                colors.Add(ColorDataExtensions.ToUnity(cell.Color)); 
                

                for (int i = 0; i < cell.Corners.Count; i++)
                {
                    int next = (i + 1) % cell.Corners.Count;
                    
                    vertices.Add(Vector3Extensions.ToUnity(cell.Corners[i]));
                    //colors.Add(new Color(cell.Color.R, cell.Color.G, cell.Color.B, cell.Color.A)); //fix 
                    colors.Add(ColorDataExtensions.ToUnity(cell.Sectors[i].Color)); 
                    
                    vertices.Add(Vector3Extensions.ToUnity(cell.Corners[next]));
                    //colors.Add(new Color(cell.Color.R, cell.Color.G, cell.Color.B, cell.Color.A)); //fix
                    colors.Add(ColorDataExtensions.ToUnity(cell.Sectors[i].Color)); 
                    
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

        private void OnDrawGizmos()
        {
            if (_planet == null) return;
            if( _planet.Cells == null ) return;
            foreach (Cell cell in _planet.Cells)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(Vector3Extensions.ToUnity(cell.Center), 0.008f);
                Gizmos.color = Color.red;
                foreach (Vector3Data corner in cell.Corners)
                {
                    Gizmos.DrawSphere(Vector3Extensions.ToUnity(corner), 0.004f);
                }
            }
        }
    } 
}

