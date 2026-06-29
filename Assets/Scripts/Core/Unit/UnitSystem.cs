using System.Collections.Generic;

namespace Core
{
    internal abstract class UnitSystem<TBehaviour>
    {
        protected Dictionary<Unit, TBehaviour> _instances = new Dictionary<Unit, TBehaviour>();
        internal abstract void Tick(float deltaTime);

        public virtual void Register(Unit unit, TBehaviour b)
        {
            _instances.Add(unit, b);
        }

        public virtual void Unregister(Unit unit)
        {
            _instances.Remove(unit);
        }
    }
}