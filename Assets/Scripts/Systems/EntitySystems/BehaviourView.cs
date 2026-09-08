using Core;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Systems
{
    /*
     * This class is used to display visuals of a behaviour.
     * Like rotation of a turret
     */
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