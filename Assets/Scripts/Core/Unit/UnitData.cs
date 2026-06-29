using System;
using System.Collections.Generic;

namespace Core
{
    public sealed class UnitData
    {
        public string Name { get; private set; }
        public UnitTypes UnitType { get; private set; }
        internal List<Func<Unit, Context, UnitBehaviour>> BehaviourFactories { get; }
        
        public UnitData(string name, UnitTypes type,  
            List<Func<Unit, Context, UnitBehaviour>> behaviourFactories)
        {
            Name = name;
            UnitType = type;
            BehaviourFactories = behaviourFactories;
        }
    }
}