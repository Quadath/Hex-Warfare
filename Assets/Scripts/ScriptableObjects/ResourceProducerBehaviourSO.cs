using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Behaviours;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/Behaviour/ResourceProducer")]
    public class ResourceProducerBehaviourSO: EntityBehaviourDataSO, IResourceProducerBehaviour
    {
        [SerializeField] private List<ResourceData> products;
        public List<ResourceInstance> Products => products.Select(a => a.ToCore()).ToList();
        [SerializeField] private List<ResourceData> ingridients;
        public List<ResourceInstance> Ingridients => ingridients.Select(a => a.ToCore()).ToList();
        [SerializeField] private float period;
        public float Period => period;
        public override Type BehaviourType { get; } = typeof(ResourceProducerBehaviourConfiguration);

        public override Func<Entity, Context, Behaviour> BehaviourFactory()
        {
            var conf = new ResourceProducerBehaviourConfiguration(Products, period, Ingridients);
            return (entity, ctx) => new ResourceProducerBehaviour(entity, conf, ctx);
        }
    }
}