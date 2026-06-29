using System;
using System.Collections.Generic;

namespace Core
{
    internal sealed class UnitSystemsContainer
    {
        internal MovementSystem MovementSystem { get; private set; }
        private Dictionary<Type, Action<Unit, UnitBehaviour>> _registry = new();

        internal UnitSystemsContainer()
        {
            MovementSystem = new MovementSystem();
            RegisterSystem(MovementSystem);
        }
        

        internal void Tick(float deltaTime)
        {
            MovementSystem.Tick(deltaTime);
        }
        
        internal void Register(Unit unit, UnitBehaviour behaviour)
        {
            //Register the behaviour in the corresponding system
            Get(behaviour.GetType())(unit, behaviour);
        }

        internal void Unregister(UnitBehaviour behaviour)
        {
            throw new NotImplementedException();
        }
        //generic helper
        private void RegisterSystem<TBehaviour>(UnitSystem<TBehaviour> system)
            where TBehaviour : UnitBehaviour
        {
            _registry[typeof(TBehaviour)] =
                (unit, behaviour) => system.Register(unit, (TBehaviour)behaviour);
        }
        
        private Action<Unit, UnitBehaviour> Get(Type T) => 
            _registry.TryGetValue(T, out var service) ? service : throw new Exception($"Registerer for {T} not found!");
    }
}