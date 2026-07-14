namespace Core.Behaviours
{
    internal class ResourceProducerSystem: BehaviourSystem<ResourceProducerBehaviour>
    {
        private readonly ResourceManager _resourceManager;

        internal ResourceProducerSystem(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }
        
        internal override void Tick(float deltaTime)
        {
            foreach (var pair in _instances)
            {
                var entity = pair.Key;
                var behaviour = pair.Value;
                
                behaviour.Cooldown -= deltaTime;
                if (!(behaviour.Cooldown <= 0)) continue;
                foreach (var resource in behaviour.Products)
                    _resourceManager.AddResource(resource);
                behaviour.Cooldown = behaviour.Period;
            }
        }
    }
}