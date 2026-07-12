using System;
using Core.Structs;

namespace Core
{
    //This class is container for spawn request, it may contain initial orders for Entities
    public class SpawnRequest
    {
        public readonly int Id;
        public readonly Cell Spawn;
        public readonly bool SelectOnSpawn;
        public readonly int ControlledBy;
        

        public SpawnRequest(SpawnRequestBuilder builder)
        {
            Id = builder.Id;
            Spawn = builder.Spawn;
            SelectOnSpawn = builder.SelectOnSpawn;
            ControlledBy = builder.ControlledBy;
        }
    }

    public class SpawnRequestBuilder
    {
        internal int Id { get; }
        internal Cell Spawn { get; }

        internal bool SelectOnSpawn;
        internal int ControlledBy;

        public SpawnRequestBuilder(int id, Cell spawn)
        {
            Id = id;
            Spawn = spawn;
        }

        public SpawnRequestBuilder SelectAfterSpawned()
        {
            SelectOnSpawn = true;
            return this;
        }

        public SpawnRequestBuilder ControlledByPlayer(int playerId)
        {
            ControlledBy = playerId;
            return this;
        }

        public SpawnRequest Build()
        {
            return new SpawnRequest(this);
        }
    
    }
}