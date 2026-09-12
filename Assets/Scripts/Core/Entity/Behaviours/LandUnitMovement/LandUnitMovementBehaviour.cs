using System.Collections.Generic;
using JetBrains.Annotations;

namespace Core.Behaviours
{
    //Container of data. This data is used in Tick() of corresponding system.
    public class LandUnitMovementBehaviour: Behaviour, ILandUnitMovementBehaviour
    {
        public float BaseSpeed { get; }
        
        //RUNTIME
        public Cell TargetCell {get; internal set; }
        //Used by the view
        public Cell NextCell { get; internal set; }
        [CanBeNull] public List<Cell> Path { get; internal set; }
        internal int CellIndex { get; private set; } = 1;
        
        public LandUnitMovementBehaviour(Entity owner, ILandUnitMovementBehaviour config, Context ctx = null): base(owner)
        {
            BaseSpeed = config.BaseSpeed;
        }

        internal void OnTargetCellReached(Cell c)
        {
            CellIndex++;
            NextCell = null;
            Owner.Cell.Exit(Owner);
            Owner.SetCell(c);
            Owner.SetPosition(c.Center);
        }

        internal void EndMovement()
        {
            CellIndex = 1;
            TargetCell = null;
            NextCell = null;
            Path = null;
        }
    }
}