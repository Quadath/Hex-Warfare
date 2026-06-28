using System;
using System.Collections.Generic;

namespace Core.Commands
{
    public class UnitCommands
    {
        private UnitFactory _unitFactory;
        private MovementSystem _movementSystem;
        private UnitManager _unitManager;

        internal UnitCommands(UnitFactory factory, UnitManager manager, MovementSystem movementSystem)
        {
            _unitFactory = factory;
            _unitManager = manager;
            _movementSystem = movementSystem;
        }

        public void ProvideFactories(Dictionary<UnitTypes, Func<UnitData>> factories)
        {
            _unitFactory.SetFactories(factories);
        }
        
        public Unit Spawn(UnitTypes type)
        {
            return _unitFactory.Spawn(type);
        }

        public void MoveTo(Cell target)
        {
            
        }
    }
}