using System;

namespace Core.Behaviours
{
    public class SelectionBehaviour: Behaviour, ISelectionBehaviour
    {
        public bool CanMove { get; internal set; }
        
        /*|||||||||
         * RUNTIME
         ||||||||*/
        internal event Action OnSelected;
        internal event Action OnDeselected;
        //Context contains things a Behaviour may need (e.g. another Behaviour)
        public SelectionBehaviour(Entity owner, ISelectionBehaviour conf, Context ctx = null) : base(owner)
        {
            CanMove = conf.CanMove;
        }
        
        internal void Select() => OnSelected?.Invoke();
        internal void Deselect() => OnDeselected?.Invoke();
        
        public void AddOnSelectedListener(Action action) =>
            OnSelected += action;
        public void RemoveOnSelectedListener(Action action) => 
            OnSelected -= action;
        public void AddOnDeselectedListener(Action action) => 
            OnDeselected += action;
        public void RemoveOnDeselectedListener(Action action) => 
            OnDeselected -= action;
        
    }
}