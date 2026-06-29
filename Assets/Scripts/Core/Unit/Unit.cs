using System;
using System.Collections.Generic;
using Shared;
using JetBrains.Annotations;

namespace Core
{
    public class Unit
    {
        public Cell Cell { get; private set; }
        public UnitTypes Type { get; }
        public Vector3Data Position { get; internal set; }
        private event Action<Unit> OnDeath; 
        [CanBeNull] public List<Cell> Path {get; private set;}

        public Unit(UnitTypes type, Cell spawn)
        {
            Type = type;
            Cell = spawn;
            Position = spawn.Center;
        }

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

        internal void SetCell(Cell cell)
        {
            Cell = cell;
        }

        internal void StopMoving()
        {
            Path = null;
        }
    }
}