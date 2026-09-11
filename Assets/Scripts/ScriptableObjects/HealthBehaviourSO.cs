using System;
using Core;
using Core.Behaviours;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/Behaviour/Health")]
    public class HealthBehaviourSO: EntityBehaviourDataSO, IHealthBehaviour
    {
        [SerializeField] private int maxHealth;
        public int MaxHealth => maxHealth;
        
        public override Type BehaviourType { get; } = typeof(HealthBehaviour);

        public override Func<Entity, Context, Behaviour> BehaviourFactory()
        {
            var config = new HealthBehaviourConfiguration(maxHealth);
            return config.Factory;
        }
        
    }
}