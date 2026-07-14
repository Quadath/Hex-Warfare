using System;
using System.Collections.Generic;
using Core.Structs;

namespace Core.Behaviours
{
    public class ResourceProducerBehaviour: Behaviour, IResourceProducerBehaviour
    {
        public List<ResourceInstance> Products { get; }
        public List<ResourceInstance> Ingridients { get; }
        public float Period { get; }
        internal float Cooldown { get; set; }
        
        public ResourceProducerBehaviour(Entity owner, IResourceProducerBehaviour data, Context ctx = null): base(owner, ctx)
        {
            Products = data.Products;
            Period = data.Period;
            Ingridients = data.Ingridients;
            Cooldown = Period;
        }
    }
}