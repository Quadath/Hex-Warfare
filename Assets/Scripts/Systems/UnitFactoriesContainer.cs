using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using SO;

namespace Systems
{
    public class UnitFactoriesContainer : MonoBehaviour
    {
        [SerializeField] private List<UnitDataSO> units;
        private Game _game;

        private void Start()
        {
            if (GameBootstrap.Instance.Game == null) throw new NullReferenceException();
            _game = GameBootstrap.Instance.Game;
            Dictionary<UnitTypes, Func<UnitData>> factories = units.ToDictionary(
                u => u.UnitType,
                u => u.UnitDataFactory());
            _game.UnitCommands.ProvideFactories(factories);
            _game.UnitCommands.Spawn(UnitTypes.Tank); //hard coded
        }
    }
}
