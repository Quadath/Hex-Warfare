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
        
        public ResourceProducerBehaviour(Entity owner, IResourceProducerBehaviour data, Context ctx = null): base(owner, ctx)
        {
            Products = data.Products;
            Period = data.Period;
            Ingridients = data.Ingridients;
            Cooldown = Period;
        }
    }
}