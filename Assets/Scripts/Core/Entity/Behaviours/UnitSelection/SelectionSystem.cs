using System.Collections.Generic;
using System.Linq;

namespace Core.Behaviours
{
    //Allows to select any entities which have SelectionBehaviour attached.
    internal class SelectionSystem: BehaviourSystem<SelectionBehaviour>
    {
        private readonly List<Entity> _selectedEntities = new List<Entity>();
        internal IReadOnlyList<Entity> SelectedEntities => _selectedEntities.ToList();
        internal override void Tick(float deltaTime) {}

        internal void AddToSelection(Entity entity)
        {
            if (entity.TryGetBehaviour(typeof(SelectionBehaviour)) == null)
                DebugUtils.Message(this, "Entity has no SelectionBehaviour attached!", entity.ViewId);
            _selectedEntities.Add(entity);
            var b = (SelectionBehaviour)entity.GetBehaviour(typeof(SelectionBehaviour));
            b.Owner.AddOnDestroyedListener(HandleEntityDeath);
            b.Select();
        }

        internal void AddToSelection(List<Entity> entities)
        {
            foreach (var e in entities)
                AddToSelection(e);
        }
        
        internal void ClearSelection()
        {
            foreach (var e in _selectedEntities)
            {
                var b = (SelectionBehaviour)e.GetBehaviour(typeof(SelectionBehaviour));
                b.Owner.RemoveOnDestroyedListener(HandleEntityDeath);
                b.Deselect();
            }
            _selectedEntities.Clear();
        }

        private void HandleEntityDeath(Entity entity)
        {
            _selectedEntities.Remove(entity);
            entity.RemoveOnDestroyedListener(HandleEntityDeath);
        }
    }
}