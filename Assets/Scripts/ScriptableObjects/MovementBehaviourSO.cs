using System;
using Core;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "UnitBehaviourSO", menuName = "Unit/Behaviour/Movement")]
    public class MovementBehaviourSO: UnitBehaviourSO, IMovementBehaviourConfig
    {
        [SerializeField] private float moveSpeed;
        public float Speed => moveSpeed;

        public override Func<Unit, Context, UnitBehaviour> UnitBehaviourFactory()
        {
            return (unit, ctx) => new MovementBehaviour(unit, moveSpeed, ctx);
        }
    }
}