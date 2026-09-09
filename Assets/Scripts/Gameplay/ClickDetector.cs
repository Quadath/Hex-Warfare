using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Systems
{
    public class ClickDetector : MonoBehaviour
    {
        Ray ray;
        RaycastHit hit;
        
        public PlanetView planetView;

        private void Start()
        {
            //_unit = Unit.GetComponent<UnitView>()._unit;
        }

        void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (Physics.Raycast(ray, out hit))
                {
                    Build(hit.point);
                }
            }
        }

        private void Build(Vector3 point)
        {
            var cell = planetView.OnClicked(point);
            var sector = cell.GetClosestSector(Vector3Extensions.ToCore(point));
            GameBootstrap.Instance.Game.EntityCommands.Spawn(
                new SpawnRequestBuilder(10, cell)
                    .Building(sector)
                    .ControlledByPlayer(1)
                    .Build()
                );
        }

        private void MoveSelectedUnits(Vector3 hit)
        {
            GameBootstrap.Instance.Game.EntityCommands.MoveSelected(planetView.OnClicked(hit));
        }

        private void HightlightClickedSector()
        {
            planetView.OnClicked(hit.point).GetClosestSector
                (Vector3Extensions.ToCore(hit.point)).Highlight();
            planetView.Draw();
        }
    }
}
