using System;
using Core.Commands;

namespace Core
{
    public sealed class Game
    {
        private readonly UnitSystemsContainer _unitSystems = new();
        private readonly UnitManager _unitManager = new();
        private readonly UnitFactory _unitFactory;

        private readonly object _owner;
        
        private readonly Planet _planet = new();
        
        public UnitCommands UnitCommands { get; }
        public WorldCommands WorldCommands { get; }

        public Game(object owner)
        {
            WorldCommands = new WorldCommands(_planet); 
            
            _unitFactory = new UnitFactory(_unitSystems, _unitManager);
            UnitCommands = new UnitCommands(_unitFactory, _unitManager,  _unitSystems.MovementSystem);
            _owner = owner;
        }

        public void Tick(float deltaTime, object owner)
        {
            if (!ReferenceEquals(_owner, owner))
                throw new InvalidOperationException(); //this allows only GameBootstrap to call public void Tick()
            _unitSystems.Tick(deltaTime);
        }
    }
}