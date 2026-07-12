using System;
using System.Collections.Generic;
using System.Linq;
using Core.Behaviours;

namespace Core
{
    public sealed class EntityFactory
    {
        //storing EntityData, not Factories as long the Factories don't require any parameters from Core
        private Dictionary<int, EntityData> _entries;
        
        private readonly BehaviourSystemsContainer _behaviourSystems;
        
        //Unity follows this event and creates corresponding view 
        private event Action<Entity> OnEntityCreated; 
        
        /*Collect each request from Systems Tick() and execute after systems have been ticked. 
        It prevents collections from updating while iterating*/
        private readonly Queue<SpawnRequest> _spawnRequests = new();

        internal EntityFactory(BehaviourSystemsContainer behaviourSystems)
        {
            _behaviourSystems = behaviourSystems;
        }

        internal void QueueSpawn(SpawnRequest request)
        {
            _spawnRequests.Enqueue(request);
        }
        
        
        internal void SetFactories(List<Func<EntityData>> factories)
        {
            if (_entries != null)
                throw new InvalidOperationException("Factories have already been initialized.");
            List<EntityData> data = factories.Select(d => d()).ToList();
            _entries = data.ToDictionary(d => d.DefinitionId, d => d);
        }
        
        internal void AddOnEntityCreatedListener(Action<Entity> onEntityCreated) =>  OnEntityCreated += onEntityCreated;

        internal void Tick() => SpawnQueued();
        
        private void SpawnQueued()
        {
            while (_spawnRequests.Count > 0)
            {
                var request = _spawnRequests.Dequeue();
                int id = request.Id;
                Cell spawn = request.Spawn;
                
                EntityData data =  _entries[id];
                if (data == null) throw new NullReferenceException();
                
                Entity entity = new Entity(data, spawn, request.ControlledBy);
                foreach (var factory in data.BehaviourFactories)
                {
                    Behaviour b = factory(entity, null);
                    entity.AddBehaviour(b);
                    _behaviourSystems.Register(entity, b);
                }
                if(request.SelectOnSpawn) _behaviourSystems.SelectionSystem.AddToSelection(entity); 
                
                OnEntityCreated?.Invoke(entity);
                DebugUtils.Message(this, "Firing the OnEntityCreated event");
            }
        }
    }
}