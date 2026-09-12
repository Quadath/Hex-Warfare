using System;
using Core;
using Core.Behaviours;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/Behaviour/Sight")]
    
    public class SightBehaviourSO: EntityBehaviourDataSO, ISightBehaviour
    {
        [SerializeField] private int range;
        public int Range => range;
        public override Type BehaviourType { get; } = typeof(SightBehaviour);
        
        public override Func<Entity, Context, Behaviour> BehaviourFactory()
        {
            var config = new SightBehaviourConfiguration(Range);
            return config.Factory;
        }
    }
}