using System.Collections.Generic;
using Core;

namespace Core.Behaviours
{
    public interface IResourceProducerBehaviour
    {
        List<ResourceInstance> Products { get; }
        List<ResourceInstance> Ingridients { get; }
        float Period { get; }
    }
}