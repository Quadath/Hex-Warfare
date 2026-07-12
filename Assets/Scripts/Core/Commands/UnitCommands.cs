using System;
using System.Collections.Generic;

namespace Core.Commands
{
    /*
     * Classes of Core.Commands work as Core's API for Unity
     */
    public class UnitCommands
    {
        private readonly UnitFactory _unitFactory;
        private readonly MovementSystem _movementSystem;
        private readonly UnitManager _unitManager;

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
        
        public Unit Spawn(UnitTypes type, Cell spawn, Action<Unit> onUnitSpawned =  null)
        {
            Unit u = _unitFactory.Spawn(type, spawn);
            onUnitSpawned?.Invoke(u);
            return u;
        }
        
        public void SelectUnit(Unit unit) => _unitManager.SelectUnit(unit);

        public void MoveTo(Unit unit, Cell target) => _movementSystem.SetTarget(unit, target);

        public void MoveSelected(Cell target)
        {
            foreach (Unit u in _unitManager._selectedUnits)
            {
                _movementSystem.SetTarget(u, target);
            }
        }
    }
}