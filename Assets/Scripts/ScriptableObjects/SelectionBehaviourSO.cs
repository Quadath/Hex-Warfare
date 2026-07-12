using System;
using Core;
using Core.Behaviours;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/Behaviour/Selection")]
    public class SelectionBehaviourSO: EntityBehaviourDataSO
    {
        public override Type BehaviourType { get; } = typeof(SelectionBehaviour);
        [SerializeField] private bool canMove;

        public override Func<Entity, Context, Behaviour> BehaviourFactory()
        {
            return (entity, ctx) => new SelectionBehaviour(entity, canMove, ctx);
        }
    }
}