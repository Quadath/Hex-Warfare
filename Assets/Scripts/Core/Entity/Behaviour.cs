using System;

namespace Core
{
    public abstract class Behaviour
    {
        public readonly Entity Owner;
        
        protected Behaviour(Entity owner)
        {
            Owner = owner;
        }

        internal virtual void Init() {}
    }
}