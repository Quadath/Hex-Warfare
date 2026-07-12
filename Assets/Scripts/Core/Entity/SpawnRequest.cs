using System;
using Shared;

namespace Core
{
    //This class is container for spawn request, it may contain initial orders for Entities
    public class SpawnRequest
    {
        public readonly int Id;
        public readonly Cell Spawn;
        public readonly Vector3Data spawnPos;
        public readonly bool SelectOnSpawn = false;
        

        public SpawnRequest(int id, Cell spawn, bool SelectOnSpawn = false)
        {
            Id = id;
            Spawn = spawn;
            spawnPos = spawn.Center;
            this.SelectOnSpawn = SelectOnSpawn;
        }

        internal SpawnRequest(int id, Vector3Data spawn)
        {
            throw new NotImplementedException();
        }
    }
}