using UnityEngine;
using Core;

namespace Gameplay
{
    public abstract class GameplaySystem: MonoBehaviour
    {
        private GameplaySystemsManager _manager;

        public abstract void Tick();
        
        internal virtual void Select()
        {
            DebugUtils.Message(this, "Selected " + this.GetType().Name);
        }

        internal virtual void Deselect()
        {
            DebugUtils.Message(this, "Selected " + this.GetType().Name);
        }

        public void SetManager(GameplaySystemsManager manager)
        {
            if(_manager != null) throw new System.Exception("Can't set manager more than once");
            _manager = manager;
        }
    }
}