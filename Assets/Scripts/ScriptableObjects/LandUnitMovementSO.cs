using System;
using Core;
using Core.Behaviours;
using Behaviour = Core.Behaviour;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/Behaviour/LandUnitMovement")]
    public class LandUnitMovementSO: EntityBehaviourDataSO, ILandUnitMovementBehaviour
    {
        [SerializeField] private float baseSpeed;
        public float BaseSpeed => baseSpeed;

        public override Type BehaviourType { get; } = typeof(LandUnitMovementBehaviour);

        public override Func<Entity, Context, Behaviour> BehaviourFactory()
        {
            var config = new LandUnitMovementBehaviourConfiguration(BaseSpeed);
            return config.Factory;
        }
    }
}