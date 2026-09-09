using System;
using System.Collections.Generic;
using Core.Behaviours;

namespace Core.Commands
{
    /*
     * Classes of Core.Commands work as Core's API for Unity
     */
    public class EntityCommands
    {
        private readonly EntityFactory _factory;
        private readonly SelectionSystem _selectionSystem;
        private readonly LandUnitMovementSystem _landUnitMovementSystem;
        
        internal EntityCommands(EntityFactory factory, SelectionSystem selectionSystem, LandUnitMovementSystem landUnitMovementSystem)
        {
            _factory = factory;
            _selectionSystem = selectionSystem;
            _landUnitMovementSystem = landUnitMovementSystem;
        }

        public void ProvideFactories(List<Func<EntityData>> factories)
        {
            _factory.SetFactories(factories);
        }
        
        public void Spawn(SpawnRequest request)
        {
            if (request.Sector is { Building: not null }) return;
            _factory.QueueSpawn(request);
        }
        

        public void SubscribeToOnEntityCreated(Action<Entity, SpawnRequest> onEntityCreated) 
            => _factory.AddOnEntityCreatedListener(onEntityCreated);
        
        public void SelectEntity(Entity ent) => _selectionSystem.AddToSelection(ent);
        public void SelectEntities(List<Entity> entities) => _selectionSystem.AddToSelection(entities);
        public void ClearEntitySelection() => _selectionSystem.ClearSelection();
        
        public void MoveTo(Entity entity, Cell target) => _landUnitMovementSystem.SetTarget(entity, target);
        
        public void MoveSelected(Cell target)
        {
            foreach (Entity e in _selectionSystem.MovingSelection)
            {
                _landUnitMovementSystem.SetTarget(e, target);
            }
        }
    }
}