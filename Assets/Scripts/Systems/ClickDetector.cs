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
                    planetView.OnClicked(hit.point).GetClosestSector
                        (Vector3Extensions.ToCore(hit.point)).Highlight();
                    planetView.Draw();
                    //GameBootstrap.Instance.Game.UnitCommands.MoveSelected(target);

                    // List<Cell> path = AStar.FindPath(_unit.Cell == null ? 
                    //     planetView._planet.FindClosestCell(Vector3Extensions.ToCore(Unit.transform.position)) : _unit.Cell, target, 1);
                    // if (path == null) return;
                    // _unit.SetPath(path);
                }
            }
        }
    }
}
