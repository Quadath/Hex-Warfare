using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using SO;
using UnityEngine;

namespace Systems
{
    //Provides factories for entities to core.
    public class EntityFactoriesProvider: UnitySystem
    {
        [SerializeField] private EntityDataSORegistry registry;

        public PlanetView PlanetView;

        public override void Init(Game game)
        {
            game.EntityCommands.ProvideFactories
                (registry.Entries.Select(s => s.EntityDataFactory()).ToList());

            var cell = PlanetView.OnClicked(new Vector3(0.3f, 0.2f, -1));
            game.EntityCommands.Spawn(
                new SpawnRequestBuilder(1, cell)
                    .SelectAfterSpawned()
                    .ControlledByPlayer(1)
                    .Build()
                );
            var cell1 = PlanetView.OnClicked(new Vector3(0.4f, 0.2f, -1));
            game.EntityCommands.Spawn(
                new SpawnRequestBuilder(1, cell1)
                    .SelectAfterSpawned()
                    .ControlledByPlayer(1)
                    .Build()
            );
        }

        private void Validate()
        {
            throw new NotImplementedException();
        }
    }
}