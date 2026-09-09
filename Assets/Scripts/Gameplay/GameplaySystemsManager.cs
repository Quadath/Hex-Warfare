using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class GameplaySystemsManager: MonoBehaviour
    {
        [SerializeField] protected List<GameplaySystem> systems;
        [SerializeField] private GameplaySystem selectedSystem;

        private void Update()
        {
            selectedSystem.Tick();
        }
    }
}