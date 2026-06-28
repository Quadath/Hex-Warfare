using System;
using Core;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "UnitBehaviourSO", menuName = "Unit/Behaviour")]

    public abstract class UnitBehaviourSO : ScriptableObject
    {
        public abstract Func<Unit, Context, UnitBehaviour> UnitBehaviourFactory();
    }
}