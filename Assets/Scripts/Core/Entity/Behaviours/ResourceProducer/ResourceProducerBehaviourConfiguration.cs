using System.Collections.Generic;
using Core.Structs;

namespace Core.Behaviours
{
    public class ResourceProducerBehaviourConfiguration: IResourceProducerBehaviour
    {
        public List<ResourceInstance> Products { get; }
        public List<ResourceInstance> Ingridients { get; }
        public float Period { get; }

        public ResourceProducerBehaviourConfiguration(List<ResourceInstance> products, float period, List<ResourceInstance> ingridients = null)
        {
            Products = products;
            Period =  period;
            Ingridients = ingridients;
        }
    }
}