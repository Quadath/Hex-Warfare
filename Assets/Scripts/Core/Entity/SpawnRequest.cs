using System;
using Core.Structs;

namespace Core
{
    //This class is container for spawn request, it may contain initial orders for Entities
    public class SpawnRequest
    {
        //COMMON
        internal readonly int Id;
        internal readonly Cell Spawn;
        internal Vector3Data SpawnPosition;
        internal readonly int ControlledBy;
        
        //BUILDING
        public readonly bool IsABuilding;
        public readonly Cell.Sector Sector;
        
        //ORDERS
        public readonly bool SelectOnSpawn;
        
        

        public SpawnRequest(SpawnRequestBuilder builder)
        {
            Id = builder.Id;
            Spawn = builder.Spawn;
            SelectOnSpawn = builder.SelectOnSpawn;
            ControlledBy = builder.ControlledBy;
            IsABuilding =  builder.IsABuilding;
            Sector = builder.Sector;
        }
    }

    public class SpawnRequestBuilder
    {
        //COMMON
        internal readonly int Id;
        internal readonly Cell Spawn;
        internal readonly Vector3Data SpawnPosition;
        internal int ControlledBy;
        //ORDERS
        internal bool SelectOnSpawn;

        //BUILDING
        internal bool IsABuilding;
        internal Cell.Sector Sector;
        

        public SpawnRequestBuilder(int id, Cell spawn = null)
        {
            Id = id;
            Spawn = spawn;
        }
        public SpawnRequestBuilder(int id, Vector3Data spawnPosition)
        {
            Id = id;
            SpawnPosition = spawnPosition;
        }

        public SpawnRequestBuilder Building(Cell.Sector sector)
        {
            IsABuilding = true;
            Sector = sector;
            return this;
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