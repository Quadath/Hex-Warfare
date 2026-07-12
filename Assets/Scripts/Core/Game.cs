using System;
using Core.Commands;

namespace Core
{
    public sealed class Game
    {
        private readonly BehaviourSystemsContainer _behaviourSystems = new();
        private readonly EntityFactory _entityFactory;

        private readonly object _owner;
        
        private readonly Planet _planet = new();
        
        public WorldCommands WorldCommands { get; }
        public EntityCommands EntityCommands { get; }

        public Game(object owner)
        {
            DebugUtils.Message(this, "Initializing...");
            WorldCommands = new WorldCommands(_planet); 
            
            _entityFactory = new EntityFactory(_behaviourSystems);
            EntityCommands = new EntityCommands
                (_entityFactory, _behaviourSystems.SelectionSystem, _behaviourSystems.LandUnitMovementSystem);
            
            _owner = owner;
        }

        public void Tick(float deltaTime, object owner)
        {
            //Tick is exposed, but any unauthorised call throws exception
            if (!ReferenceEquals(_owner, owner))
                throw new InvalidOperationException("Unauthorised Game.Tick() call!"); 
            _behaviourSystems.Tick(deltaTime);
            _entityFactory.Tick();
        }
    }
}