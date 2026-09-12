using System;
using System.Collections.Generic;
using Core.Behaviours;

namespace Core
{
    //This class holds every BehaviourSystem and ticks them in a certain order
    internal sealed class BehaviourSystemsContainer
    {
        internal readonly LandUnitMovementSystem LandUnitMovementSystem = new();
        internal readonly ResourceProducerSystem ResourceProducerSystem;
        internal readonly SelectionSystem SelectionSystem = new();
        internal readonly HealthBehaviourSystem HealthBehaviourSystem = new();
        internal readonly SightBehaviourSystem SightBehaviourSystem = new();
        
        private readonly Dictionary<Type, Action<Entity, Behaviour>> _registry = new();

        internal BehaviourSystemsContainer(Context ctx)
        {
            var resourceManager = ctx.Resolve<ResourceManager>();
            ResourceProducerSystem = new ResourceProducerSystem(resourceManager);
            
            RegisterSystem(LandUnitMovementSystem);
            RegisterSystem(ResourceProducerSystem);
            RegisterSystem(SelectionSystem);
            RegisterSystem(HealthBehaviourSystem);
            RegisterSystem(SightBehaviourSystem);
        }
        

        internal void Tick(float deltaTime)
        {
            LandUnitMovementSystem.Tick(deltaTime);
            //HealthBehaviourSystem.Tick(deltaTime); //Has no logic
            ResourceProducerSystem.Tick(deltaTime);
            //SelectionSystem.Tick(deltaTime); //Has no logic
            //SightBehaviourSystem.Tick(deltaTime); //Has no logic
        }
        
        internal void Register(Entity entity, Behaviour behaviour)
        {
            //Register the behaviour in the corresponding system
            Get(behaviour.GetType())(entity, behaviour);
        }

        internal void Unregister(Behaviour behaviour)
        {
            //Registers unregister behaviours
        }
        //Generic helper
        private void RegisterSystem<TBehaviour>(BehaviourSystem<TBehaviour> system)
            where TBehaviour : Behaviour
        {
            _registry[typeof(TBehaviour)] =
                (unit, behaviour) => system.Register(unit, (TBehaviour)behaviour);
        }
        
        private Action<Entity, Behaviour> Get(Type T) => 
            _registry.TryGetValue(T, out var service) ? service : throw new InvalidOperationException($"Registerer for {T} not found!");
    }
}