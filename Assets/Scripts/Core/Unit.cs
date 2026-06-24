using System.Collections.Generic;
using JetBrains.Annotations;

namespace Core
{
    public class Unit
    {
        [CanBeNull] public List<Cell> Path {get; private set;}
        public Cell Cell { get; private set; }
        private Vector3Data _position;

        public void SetPath(List<Cell> path)
        {
            Path = path;
        }

        public void SetCell(Cell cell)
        {
            Cell = cell;
        }

        public void StopMoving()
        {
            Path = null;
        }
    }
}