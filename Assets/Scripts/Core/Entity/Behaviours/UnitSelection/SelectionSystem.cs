using System.Collections.Generic;
using System.Linq;

namespace Core.Behaviours
{
    internal class SelectionSystem: BehaviourSystem<SelectionBehaviour>
    {
        private readonly List<Entity> _selectedEntities = new List<Entity>();
        private readonly List<Entity> _movingSelection = new List<Entity>();
        internal IReadOnlyList<Entity> SelectedEntities => _selectedEntities.ToList();
        internal IReadOnlyList<Entity> MovingSelection => _movingSelection.ToList();
        internal override void Tick(float deltaTime) {}

        internal void AddToSelection(Entity entity)
        {
            if (entity.TryGetBehaviour(typeof(SelectionBehaviour)) == null)
                DebugUtils.Message(this, "Entity has no UnitSelectionBehaviour attached!", entity.ViewId);
            _selectedEntities.Add(entity);
            var b = (SelectionBehaviour)entity.GetBehaviour(typeof(SelectionBehaviour));
            if (b.CanMove) _movingSelection.Add(entity);
        }
        
        internal void ClearSelection()
        {
            _selectedEntities.Clear();
        }
    }
}