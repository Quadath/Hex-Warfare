using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Core
{
    internal sealed class UnitFactory
    {
        private UnitSystemsContainer _unitSystems;
        private UnitManager _unitManager;

        [CanBeNull] private Dictionary<UnitTypes, Func<UnitData>> _factories; 

        internal UnitFactory(UnitSystemsContainer unitSystems, UnitManager unitManager)
        {
            _unitSystems = unitSystems;
            _unitManager = unitManager;
        }

        internal void SetFactories(Dictionary<UnitTypes, Func<UnitData>> factories)
        {
            if (_factories != null)
                throw new InvalidOperationException("Factories have already been initialized.");
            _factories ??= factories;
        }

        internal Unit Spawn(UnitTypes type, Cell spawn, Action<Unit> onCreated = null)
        {
            if(_factories == null) 
                throw new NullReferenceException("Factories are not initialized.");
            UnitData data = _factories[type]();
            Unit unit = new(type, spawn);
            foreach (var behaviourFactory in data.BehaviourFactories)
            {
                _unitSystems.Register(unit, behaviourFactory(unit, null));
            }
            _unitManager.RegisterUnit(unit);
            onCreated?.Invoke(unit);
            return unit;
        }
    }
}