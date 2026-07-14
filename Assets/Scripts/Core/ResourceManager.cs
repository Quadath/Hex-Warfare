using System.Collections.Generic;
using System.Linq;
using Core.Structs;

namespace Core
{
    internal sealed class ResourceManager
    {
        private Dictionary<ResourceTypes, ResourceInstance> Resources { get; }

        internal ResourceManager(List<ResourceInstance> resources = null)
        {
            Resources = resources == null ? new Dictionary<ResourceTypes, ResourceInstance>() :
                    resources.ToDictionary(a => a.Type, a => a);
        }

        internal void AddResource(ResourceInstance resource)
        {
            if (Resources.TryGetValue(resource.Type, out var r)) 
                r.AddAmount(resource.Amount);
            else 
                Resources.Add(resource.Type, new ResourceInstance(resource.Type, resource.Amount));
            
            DebugUtils.Message(this, $"Have {Resources[resource.Type].Amount} of {resource.Type} now.");
        }
    }
}