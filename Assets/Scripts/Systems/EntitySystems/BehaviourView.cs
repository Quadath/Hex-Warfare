using Core;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Systems
{
    public abstract class BehaviourView: MonoBehaviour
    {
        protected Behaviour Behaviour;
        protected EntityView View;
        protected Entity Entity;

        public void Init(Behaviour behaviour, EntityView view)
        {
            Behaviour = behaviour;
            Entity = behaviour.Owner;
            View = view;
            OnInit();
        }

        protected virtual void OnInit()
        {
        } 
        public abstract void Tick();
    }
}