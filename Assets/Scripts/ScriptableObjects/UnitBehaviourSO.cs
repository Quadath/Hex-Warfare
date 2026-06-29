using System;
using Core;
using UnityEngine;

namespace SO
{
    public abstract class UnitBehaviourSO : ScriptableObject
    {
        public abstract Func<Unit, Context, UnitBehaviour> UnitBehaviourFactory();
    }
}