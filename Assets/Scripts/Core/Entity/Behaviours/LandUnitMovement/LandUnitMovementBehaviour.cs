using System.Collections.Generic;
using JetBrains.Annotations;

namespace Core.Behaviours
{
    public class LandUnitMovementBehaviour: Behaviour, ILandUnitMovementBehaviour
    {
        public float BaseSpeed { get; }
        /*|||||||||
         * RUNTIME
         ||||||||*/
        public Cell TargetCell {get; internal set; }
        public Cell NextCell { get; internal set; }
        [CanBeNull] public List<Cell> Path { get; internal set; }
        internal int CellIndex { get; set; } = 0;
        
        public LandUnitMovementBehaviour(Entity owner, float baseSpeed, Context ctx = null): base(owner, ctx)
        {
            BaseSpeed = baseSpeed;
        }

        internal void OnTargetCellReached(Cell c)
        {
            CellIndex++;
            NextCell = null;
            Owner.SetCell(c);
            Owner.SetPosition(c.Center);
        }
    }
}