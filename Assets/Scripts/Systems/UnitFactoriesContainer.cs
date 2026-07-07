using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using SO;

namespace Systems
{
    public class UnitFactoriesContainer : UnitySystem
    {
        [SerializeField] private List<UnitDataSO> units;

        [SerializeField] private PlanetView planetView;
        [SerializeField] private UnitViewFactory unitViewFactory;
        
        private Game _game;

        public override void Init(Game game)
        {
            if (GameBootstrap.Instance.Game == null) throw new NullReferenceException();
            Dictionary<UnitTypes, Func<UnitData>> factories =
                units.ToDictionary(k => k.UnitType, v => v.UnitDataFactory());
            game.UnitCommands.ProvideFactories(factories);
            Unit u = game.UnitCommands.Spawn(UnitTypes.Tank,
                planetView.OnClicked(new Vector3(0, 0, -3)),
                OnCreated); //hard coded);
            GameBootstrap.Instance.Game.UnitCommands.SelectUnit(u);
        }

        private void OnCreated(Unit unit)
        {
            unitViewFactory.CreateView(units.Last(so => so.UnitType == unit.Type), unit);
        }
    }
}
