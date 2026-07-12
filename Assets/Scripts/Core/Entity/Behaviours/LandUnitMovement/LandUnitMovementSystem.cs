using System;
using System.Collections.Generic;
using Shared;

namespace Core.Behaviours
{
    internal class LandUnitMovementSystem: BehaviourSystem<LandUnitMovementBehaviour>
    {
        internal override void Tick(float deltaTime)
        {
            foreach (var pair in base._instances)
            {
                var entity = pair.Key;
                var behaviour = pair.Value;
                if (behaviour.TargetCell == null) continue;
                
                behaviour.Path ??= behaviour.Path = AStar.FindPath(entity.Cell, behaviour.TargetCell);
                if (behaviour.Path == null) continue;
                DebugUtils.Sphere(entity.Position, 0.005f);
                if (behaviour.Path != null && behaviour.CellIndex < behaviour.Path.Count)
                {
                    Cell targetCell = behaviour.Path[behaviour.CellIndex];
                    //UnitView references this
                    //unit.ToLookAt = targetCell.Center; 
                    DebugUtils.Line(entity.Position, targetCell.Center);
                        
                    if ((entity.Position - targetCell.Center).SqrMagnitude < 0.0001f)
                    {
                        behaviour.CellIndex++;
                        entity.SetCell(targetCell);
                        behaviour.TargetCell = targetCell;
                    }
                    else
                    {
                        Vector3Data delta = targetCell.Center - entity.Position;
                        float distance = delta.SqrMagnitude;
                        float step = deltaTime * behaviour.BaseSpeed;

                        if (distance <= Math.Pow(step, 2))
                        {
                            entity.SetPosition(targetCell.Center);
                            entity.SetCell(targetCell);
                            behaviour.CellIndex++;
                        }
                        else
                        {
                            entity.Move(delta.Normalized * step);
                        }
                    }
                }
                else
                {
                    behaviour.CellIndex = 0;
                    behaviour.TargetCell = null;
                    behaviour.Path = null;
                }
            }
        }

        internal void SetTarget(Entity entity, Cell targetCell)
        {
            DebugUtils.Message(this, "New target set", entity.ViewId);
            LandUnitMovementBehaviour b = _instances.GetValueOrDefault(entity);
            if (b == null) return;
            b.TargetCell = targetCell;
        }
    }
}