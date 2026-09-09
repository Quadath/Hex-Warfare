using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Gameplay
{
    public sealed class GameplaySystemsManager: MonoBehaviour
    {
        [SerializeField] private GameplaySystem selectedSystem;

        private void Update()
        {
            selectedSystem.Tick();
        }

        internal void SetActiveSystem(GameplaySystem system)
        {
            DebugUtils.Message(this, "Selected system: " + system.GetType().Name);
            selectedSystem.Deselect();
            selectedSystem = system;
            selectedSystem.Select();
        }
    }
}