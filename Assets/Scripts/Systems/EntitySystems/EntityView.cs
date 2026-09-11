using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Systems
{
    public class EntityView:  MonoBehaviour
    {
        public Entity Entity { get; private set; }

        public readonly List<BehaviourView> BehaviourViews = new();

        public void SetEntity(Entity entity)
        {
            if(Entity != null) throw new InvalidOperationException("Entity is already set");
            Entity = entity;
            Entity.AddOnDestroyedListener((e) => Destroy(gameObject));
        }

        private void FixedUpdate()
        {
            BehaviourViews.ForEach(behaviourView => behaviourView.Tick()); 
        }
    }
}