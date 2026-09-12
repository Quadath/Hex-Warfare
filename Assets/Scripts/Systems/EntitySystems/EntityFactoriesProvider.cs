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

            //Hard-coded spawn 
            //MOVE IT AWAY
            var cell = GetNotWaterNeighbor(PlanetView.OnClicked(new Vector3(0.3f, 0.2f, -1)));
            game.EntityCommands.Spawn(
                new SpawnRequestBuilder(1, cell)
                    .ControlledByPlayer(1)
                    .Build()
                );
            var cell1 = GetNotWaterNeighbor(PlanetView.OnClicked(new Vector3(0.4f, 0.2f, -1)));
            game.EntityCommands.Spawn(
                new SpawnRequestBuilder(1, cell1)
                    .ControlledByPlayer(1)
                    .Build()
            );
            var cell2 = GetNotWaterNeighbor(PlanetView.OnClicked(new Vector3(-0.4f, -0.2f, -0.8f)));
            game.EntityCommands.Spawn(     
                new SpawnRequestBuilder(1, cell2)
                    .ControlledByPlayer(2)
                    .Build()
            );
        }

        private Cell GetNotWaterNeighbor(Cell c)
        {
            if (!c.IsWater) return c;
            foreach (var n in c.Neighbors)
            {
                if (!n.IsWater) return n;
            }

            foreach (var n in c.Neighbors)
            {
                foreach (var n1 in c.Neighbors)
                {
                    if (!n1.IsWater) return n;
                }
            }

            Cell c1 = null;
            for (int i = 0; i < 50; i++)
            {
                c1 = c.Neighbors[i];
                if (!c1.IsWater) return c;
            }

            return c1;
        }
        private void Validate()
        {
            throw new NotImplementedException();
        }
    }
}