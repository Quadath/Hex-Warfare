using System.Collections.Generic;
using System.Linq;

namespace Core.Behaviours
{
    public class SightBehaviour: Behaviour, ISightBehaviour
    {
        public int Range { get; }
        private readonly HashSet<Cell> _watchedCells = new();
        private readonly HashSet<Entity> _entitiesInSight = new();
        internal SightBehaviour(Entity owner, ISightBehaviour config, Context ctx = null) : base(owner)
        {
            Range = config.Range;
        }

        internal override void Init()
        {
            Owner.AddOnCellChangedListener(UpdateSightZone);
            Owner.AddOnDestroyedListener(HandleDeath);
            UpdateSightZone(Owner.Cell);
        }

        void UpdateSightZone(Cell c)
        {
            var newCells = c.GetCellsInRange(c, Range);
            
            //ToList copies the collection, so I'm free to modify _watchedCells while iterating
            foreach (var oldCell in _watchedCells.ToList())
            {
                if (newCells.Contains(oldCell))
                    continue;

                oldCell.RemoveEntityEnteredListener(EntityEntered);
                oldCell.RemoveEntityExitedListener(EntityExited);
                
                _watchedCells.Remove(oldCell);
                if (oldCell.entities.Count > 0)
                {
                    DebugUtils.Message(this, "Entities in the old cell: " + oldCell.entities.Count);
                    foreach (var e in oldCell.entities)
                    {
                        LoseEntityFromSight(e);
                    }
                }
            }

            foreach (var cell in newCells)
            {
                if (!_watchedCells.Add(cell))
                    continue;

                cell.AddEntityEnteredListener(EntityEntered);
                cell.AddEntityExitedListener(EntityExited);
                if (cell.entities.Count > 0)
                    foreach (var e in cell.entities)
                    {
                        SpotEntity(e);
                    }
            }
            foreach (var c1 in _watchedCells)
            {
                DebugUtils.Sphere(c1.Center, 0.01f, 1.5f);
            }
            DebugUtils.Message(this, "SightZone updated", Owner.ViewId);
            //DebugUtils.Message(this, "Watching " + _watchedCells.Count + " cells");
        }

        private void SpotEntity(Entity entity)
        { 
            _entitiesInSight.Add(entity);
            DebugUtils.Message(this, "Spotted entity: " + entity.Name + " |" + entity.ViewId + "|", Owner.ViewId);
        }

        private void LoseEntityFromSight(Entity entity)
        { 
            _entitiesInSight.Remove(entity);
            DebugUtils.Message(this, entity.Name +  entity.Name + " |" + entity.ViewId + "|"+ " has left detection range", Owner.ViewId);
        }

        private void HandleDeath(Entity entity)  
        {
            foreach (var cell in _watchedCells)
            {
                cell.RemoveEntityEnteredListener(EntityEntered);
                cell.RemoveEntityExitedListener(EntityExited);
            }
            _watchedCells.Clear();
            Owner.RemoveOnCellChangedListener(UpdateSightZone);
            Owner.RemoveOnDestroyedListener(HandleDeath);
        }

        private void EntityEntered(Entity entity)
        {
            if (!_entitiesInSight.Contains(entity))
                SpotEntity(entity);
        }

        private void EntityExited(Entity entity)
        {
            if(!_watchedCells.Contains(entity.Cell))
                LoseEntityFromSight(entity);
        }
    }
}