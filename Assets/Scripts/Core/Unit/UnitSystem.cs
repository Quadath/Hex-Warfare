using System.Collections.Generic;

namespace Core
{
    internal abstract class UnitSystem
    {
        private List<UnitBehaviour> _instances;
        public abstract void Tick(float deltaTime);

        public virtual void Register(UnitBehaviour b)
        {
            _instances.Add(b);
        }

        public virtual void Unregister(UnitBehaviour b)
        {
            _instances.Remove(b);
        }
    }
}