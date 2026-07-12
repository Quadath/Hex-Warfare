using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using SO;
using UnityEngine;

namespace Systems
{
    public class EntityFactoriesProvider: UnitySystem
    {
        [SerializeField] private EntityDataSORegistry registry;

        public PlanetView PlanetView;

        public override void Init(Game game)
        {
            game.EntityCommands.ProvideFactories
                (registry.Entries.Select(s => s.EntityDataFactory()).ToList());
            
            game.EntityCommands.Spawn(new SpawnRequest(1, PlanetView.OnClicked(new Vector3(0.3f, 0.2f, -1)), true));
        }

        private void Validate()
        {
            throw new NotImplementedException();
        }
    }
}