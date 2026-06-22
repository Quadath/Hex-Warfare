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
        }
    }
}