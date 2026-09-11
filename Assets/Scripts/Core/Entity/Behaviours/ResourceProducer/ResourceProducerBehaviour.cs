using System.Collections.Generic;

namespace Core.Behaviours
{
    //Container of data. This data is used it Tick() of corresponding system.
    public class ResourceProducerBehaviour: Behaviour, IResourceProducerBehaviour
    {
        public List<ResourceInstance> Products { get; }
        public List<ResourceInstance> Ingridients { get; }
        public float Period { get; }
        public float Cooldown { get; internal set; }
        
        internal ResourceProducerBehaviour(Entity owner, IResourceProducerBehaviour config, Context ctx = null): base(owner)
        {
            Products = config.Products;
            Period = config.Period;
            Ingridients = config.Ingridients;
            Cooldown = Period;
        }
    }
}