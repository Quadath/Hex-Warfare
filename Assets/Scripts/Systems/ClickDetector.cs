using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

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
                    Cell target = planetView.OnClicked(hit.point);
                    GameBootstrap.Instance.Game.UnitCommands.MoveSelected(target);
                    Debug.Log("Move Order");
                    // List<Cell> path = AStar.FindPath(_unit.Cell == null ? 
                    //     planetView._planet.FindClosestCell(Vector3Extensions.ToCore(Unit.transform.position)) : _unit.Cell, target, 1);
                    // if (path == null) return;
                    // _unit.SetPath(path);
                }
            }
        }
    }
}
