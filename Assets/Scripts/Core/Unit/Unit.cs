using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Core
{
    public class Unit
    {
        [CanBeNull] public List<Cell> Path {get; private set;}
        public Cell Cell { get; private set; }
        private Vector3Data _position;
        private event Action<Unit> OnDeath;

        public void SetPath(List<Cell> path)
        {
            Path = path;
        }
        
        public void AddOnDeathListener(Action<Unit> listener)
        {
            OnDeath += listener;
        }
        public void RemoveDeathListener(Action<Unit> listener)
        {
            OnDeath -= listener;
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