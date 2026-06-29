using System.Collections.Generic;

namespace Core
{
    public class UnitManager
    {
        private List<Unit> _units = new();
        internal List<Unit> _selectedUnits { get; } = new();
        
        //public Unit GetClosestUnit(Vector3Data pos)
        
        internal void RegisterUnit(Unit unit) => _units.Add(unit);

        internal void SelectUnit(Unit unit)
        {
            _selectedUnits.Add(unit);
        }
    }
}