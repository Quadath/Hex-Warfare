using System.Collections.Generic;
using Core;

namespace Core.Behaviours
{
    //Interfaces DO NOT declare runtime properties
    public interface IResourceProducerBehaviour
    {
        List<ResourceInstance> Products { get; }
        List<ResourceInstance> Ingridients { get; }
        float Period { get; }
    }
}