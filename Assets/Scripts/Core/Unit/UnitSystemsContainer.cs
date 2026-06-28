using System;
using System.Collections.Generic;

namespace Core
{
    internal sealed class UnitSystemsContainer
    {
        internal MovementSystem MovementSystem { get; private set; }
        private Dictionary<Type, Action<UnitBehaviour>> _registry = new();

        internal UnitSystemsContainer()
        {
            MovementSystem = new MovementSystem();
            _registry[typeof(MovementBehaviour)] = behaviour =>
            {
                MovementSystem.Register(behaviour);
            };
        }

        public void Register(UnitBehaviour behaviour)
        {
            Get(typeof(MovementBehaviour))(behaviour);
        }

        public void Unregister(UnitBehaviour behaviour)
        {
            throw new NotImplementedException();
        }
        
        private Action<UnitBehaviour> Get(Type T) => 
            _registry.TryGetValue(T, out var service) ? service : throw new Exception($"Registerer for {T} not found!");
    }
}