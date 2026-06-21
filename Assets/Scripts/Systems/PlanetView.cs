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

        void Start()
        {
            var (vertices, triangles) = Icosphere.CreateIcosphere(subdivisions, radius);

            List<Cell> cells = DualGrid.Generate(vertices, triangles, radius);

            var mesh = Icosphere.BuildMesh(cells);

            GetComponent<MeshFilter>().mesh = mesh;
        }
    } }

