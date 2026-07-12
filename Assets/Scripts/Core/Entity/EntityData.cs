using System;
using System.Collections.Generic;

namespace Core
{
    /*
     * an IMMUTABLE container of data needed for entity creation
     * !IS NOT SUPPOSED FOR RUNTIME STATE!
     */
    public class EntityData
    {
        //KEEP READONLY
        public readonly string Name;
        public readonly int DefinitionId;
        
        public readonly List<Func<Entity, Context, Behaviour>> BehaviourFactories;

        public EntityData(string name, int definitionId, 
            List<Func<Entity, Context, Behaviour>> behaviourFactories)
        {
            Name = name;
            DefinitionId = definitionId;
            BehaviourFactories = behaviourFactories;
        }
    }
}