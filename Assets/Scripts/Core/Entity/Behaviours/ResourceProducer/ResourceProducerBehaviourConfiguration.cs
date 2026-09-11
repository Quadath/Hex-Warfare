using System.Collections.Generic;
using Core.Structs;

namespace Core.Behaviours
{
    /*
     * A data container which is used for creation of corresponding behaviour.
     * This prevents Unity from direct creating of Behaviour instances.
     * It also helps to prevent Unity from providing runtime data.
     */
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

        public ResourceProducerBehaviour Factory(Entity entity, Context ctx)
            => new ResourceProducerBehaviour(entity, this, ctx);
    }
}