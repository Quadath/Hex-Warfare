using System.Collections.Generic;
using JetBrains.Annotations;

namespace Core
{
    public class MovementBehaviour: UnitBehaviour, IMovementBehaviourConfig
    {
        public float Speed { get; }
        
        //RUNTIME STATE
        public Cell TargetCell {get; internal set; }
        [CanBeNull] public List<Cell> Path { get; internal set; }
        internal int CellIndex { get; set; } = 0;
        public MovementBehaviour(Unit owner, float speed, Context ctx = null) : base(owner)
        {
            Speed = speed;
        }
    }
}