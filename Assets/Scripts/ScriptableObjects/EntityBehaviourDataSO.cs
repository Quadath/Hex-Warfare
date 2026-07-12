using System;
using Core;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace SO
{
    public abstract class EntityBehaviourDataSO: ScriptableObject
    {
        public abstract Func<Entity, Context, Behaviour> BehaviourFactory();
        public abstract Type BehaviourType { get; }
        //Might be left null, some Behaviour need no view
        [SerializeField] private GameObject behaviourView;
        public GameObject BehaviourView => behaviourView;
    }
}