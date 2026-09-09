using Core;
using Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Gameplay
{
    public class BuildingSystem: GameplaySystem
    {
        [SerializeField] private PlanetView planetView;
        
        private Ray _ray;
        private RaycastHit _hit;
        public override void Tick()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (Physics.Raycast(_ray, out _hit))
                {
                    Build(_hit.point);
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
    }
}