using Core.Behaviours;
using UnityEngine;

namespace Systems.Views
{
    public class SelectableView: BehaviourView
    {
        private SelectionBehaviour _behaviour;
        [SerializeField] private GameObject orb;
        protected override void OnInit()
        {
            _behaviour = Behaviour as SelectionBehaviour;
            _behaviour.AddOnSelectedListener(() => orb.SetActive(true));
            _behaviour.AddOnDeselectedListener(() => orb.SetActive(false));
        }
        public override void Tick()
        {
            
        }
    }
}