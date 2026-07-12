using System.Collections.Generic;

namespace Core
{
    internal abstract class BehaviourSystem<TBehaviour>
    {
        protected Dictionary<Entity, TBehaviour> _instances = new();

        internal abstract void Tick(float deltaTime);

        internal virtual void Register(Entity entity, TBehaviour behaviour)
        {
            _instances.Add(entity, behaviour);
        }

        internal virtual void Unregister(Entity entity)
        {
            _instances.Remove(entity);
        }
    }
}