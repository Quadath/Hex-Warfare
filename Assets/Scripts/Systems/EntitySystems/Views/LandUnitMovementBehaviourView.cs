using System;
using Core;
using Core.Behaviours;
using UnityEngine;
using Utils;

namespace Systems.Views
{
    public class LandUnitMovementBehaviourView: BehaviourView
    {
        private Transform _parent;
        private LandUnitMovementBehaviour _behaviour;
        protected override void OnInit()
        {
            _parent = View.transform;
            _behaviour = Behaviour as LandUnitMovementBehaviour;
            LookToClosestPole();
            
        }
        public override void Tick()
        {
            _parent.position = Vector3Extensions.ToUnity(Entity.Position);
            
            if(_behaviour.TargetCell == null) return;
            Vector3 up = _parent.position.normalized;
            Vector3 dir = Vector3Extensions.ToUnity(_behaviour.TargetCell.Center - Entity.Position); 
            Quaternion targetRotation = Quaternion.LookRotation(dir, up);
            _parent.rotation = Quaternion.Slerp(
                _parent.rotation,
                targetRotation,
                2f * Time.deltaTime
            );
        }

        private void LookToClosestPole()
        {
            var pos = Vector3Extensions.ToUnity(Entity.Position);
            var absolute = pos.z == 0 ? 1 : pos.z / Math.Abs(pos.z);
            var vertical = new Vector3(0, absolute, 0);
            var crossed = Vector3.Cross(pos, vertical);
            var lookDirection = Vector3.Cross(crossed, pos) / Vector3.Cross(crossed, pos).magnitude;
            
            _parent.rotation = Quaternion.LookRotation(lookDirection,transform.position);
            DebugUtils.Message(this, "Looking to closest pole", gameObject.GetInstanceID());
        }
    }
}