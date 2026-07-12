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
        public int? ViewInstanceId { get; private set; } //DEBUG
        public Vector3Data Position { get; internal set; }
        public Vector3Data ToLookAt { get; internal set; } //for UnityView, not Core!
        private event Action<Unit> OnDeath; 

        public void SetViewInstanceId(int? viewInstanceId)
        {
            ViewInstanceId ??= viewInstanceId;
        }
        
        public Unit(UnitTypes type, Cell spawn)
        {
            Type = type;
            Cell = spawn;
            Position = spawn.Center;
            ToLookAt = spawn.Neighbors[0].Center; //look at some cell on spawn
        }
        
        // public void AddOnDeathListener(Action<ITargetable> listener)
        // {
        //     OnDeath += listener;
        // }
        // public void RemoveOnDeathListener(Action<ITargetable> listener)
        // {
        //     OnDeath -= listener;
        // }

        internal void SetCell(Cell cell)
        {
            Cell = cell;
        }
    }
}