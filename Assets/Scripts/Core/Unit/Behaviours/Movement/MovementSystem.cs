using System;
using System.Collections.Generic;
using Shared;

namespace Core
{
    internal class MovementSystem: UnitSystem<MovementBehaviour>
    {
        internal override void Tick(float deltaTime)
        {
            foreach (var pair in base._instances)
            {
                var unit = pair.Key;
                var behaviour = pair.Value;
                if (behaviour.TargetCell == null) continue;
                
                behaviour.Path ??= behaviour.Path = AStar.FindPath(unit.Cell, behaviour.TargetCell);
                if (behaviour.Path == null) continue;
                DebugDraw.Sphere(unit.Position, 0.005f);
                if (behaviour.Path != null && behaviour.CellIndex < behaviour.Path.Count)
                {
                    Cell targetCell = behaviour.Path[behaviour.CellIndex];
                    DebugDraw.Line(unit.Position, targetCell.Center);
                        
                    if ((unit.Position - targetCell.Center).SqrMagnitude < 0.0001f)
                    {
                        behaviour.CellIndex++;
                        unit.SetCell(targetCell);
                    }
                    else
                    {
                        Vector3Data delta = targetCell.Center - unit.Position;
                        float distance = delta.SqrMagnitude;
                        float step = deltaTime * behaviour.Speed;

                        if (distance <= Math.Pow(step, 2))
                        {
                            unit.Position = targetCell.Center;
                            behaviour.CellIndex++;
                        }
                        else
                        {
                            unit.Position += delta.Normalized * step;
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

        internal void SetTarget(Unit unit, Cell targetCell)
        {
            MovementBehaviour b = _instances.GetValueOrDefault(unit);
            if (b == null) return;
            b.TargetCell = targetCell;
        }
    }
}