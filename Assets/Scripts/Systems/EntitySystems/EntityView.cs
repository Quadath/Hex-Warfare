using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Systems
{
    public class EntityView:  MonoBehaviour
    {
        private Entity _entity;
        
        public readonly List<BehaviourView> BehaviourViews = new();

        public void SetEntity(Entity entity)
        {
            if(_entity != null) throw new InvalidOperationException("Entity is already set");
            _entity = entity;
        }

        private void FixedUpdate()
        {
            BehaviourViews.ForEach(behaviourView => behaviourView.Tick()); 
        }
    }
}