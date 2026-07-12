using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Core.Structs;

namespace Core
{
    public class Entity
    {
        public string Name { get; }
        public int DefinitionId { get; }
        public int? ViewId { get; private set; }
        public Cell Cell { get; private set; }
        public Vector3Data Position { get; private set; }
        public int ControlledBy { get; }
        
        private event Action<Entity> OnDestroyed;
        
        //Dictionary allows O(1) lookup and prevents behaviours from duplicating.
        private readonly Dictionary<Type, Behaviour> _behaviours = new();

        internal Entity(EntityData data, Cell spawn, int controlledBy = 0)
        {
            DefinitionId = data.DefinitionId;
            Name = data.Name;
            Cell = spawn;
            Position = spawn.Center;
            ControlledBy = controlledBy;
        }
        
        internal void SetPosition(Vector3Data position) => Position = position;
        internal void SetCell(Cell cell)
        {
            Cell = cell;
            cell.Occupy(ControlledBy);
        }

        internal void AddBehaviour(Behaviour behaviour)
        {
            DebugUtils.Message(this, $"Trying to add behaviour {behaviour.ToString()}");
            if(_behaviours.ContainsKey(behaviour.GetType())) throw new Exception($"Behaviour {behaviour.GetType()} is already registered");
            _behaviours.Add(behaviour.GetType(), behaviour);
        }


        internal void Move(Vector3Data delta) => Position += delta;
        
        internal void Destroy()
        {
            throw new NotImplementedException();
        }
        
        public Behaviour GetBehaviour(Type type)
        {
            if(!_behaviours.TryGetValue(type, out var behaviour)) throw new Exception($"Behaviour {type} is not registered");
            return behaviour;
        }
        
        [CanBeNull]
        public Behaviour TryGetBehaviour(Type type) =>
            _behaviours.GetValueOrDefault(type);
        
        public void AddOnDestroyedListener(Action<Entity> action) => OnDestroyed += action;
        public void RemoveOnDestroyedListener(Action<Entity> action) => OnDestroyed -= action;

        
        public void SetViewId(int viewId)
        {
            if (viewId == 0) throw new InvalidOperationException($"ViewId {viewId} already set");
            ViewId = viewId;
        }
    }
}
