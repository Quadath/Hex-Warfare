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
            //Instantly subscribe to entity's death
            entity.AddOnDestroyedListener(HandleEntityDeath);
        }

        protected virtual void Unregister(Entity entity)
        {
            _instances.Remove(entity);
        }

        private void HandleEntityDeath(Entity entity)
        {
            Unregister(entity);
            entity.RemoveOnDestroyedListener(HandleEntityDeath);
        }
    }
}