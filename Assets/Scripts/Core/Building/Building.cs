using System;

namespace Core
{
    public class Building
    {
        public Cell Cell { get; }
        public BuildingTypes BuildingType { get; }
        public int? ViewInstanceId { get; private set; } //DEBUG
        private event Action<Unit> OnDeath; 
        

        internal Building(BuildingTypes buildingType, Cell cell)
        {
            Cell = cell;
            BuildingType = buildingType;
        }
    }
}