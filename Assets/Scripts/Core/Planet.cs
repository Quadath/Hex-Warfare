using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class Planet
    {
        public List<Cell> Cells { get; private set; }

        public void Generate(int subdivisions, float radius)
        {
            var (vertices, triangles) = Icosphere.CreateIcosphere(subdivisions, radius);
            Cells = DualGrid.Generate(vertices, triangles, radius);
            DualGrid.GenerateWater(Cells);
        }

        public Cell OnClicked(Vector3Data clickPos)
        {
            Cell cell = FindClosestCell(clickPos);
            //HighlightCell(cell);
            return cell;
        }

        public Cell FindClosestCell(Vector3Data position)
        {
            Cell cell = new Cell();
            float closest = float.MaxValue;

            foreach (Cell c in Cells)
            {
                float sqrDist = (c.Center - position).SqrMagnitude;
                if (sqrDist < closest)
                {
                    cell = c;
                    closest = sqrDist;
                }
            }
            return cell;
        }

        //PATHFINDING (SHOULD BE SEPARATED)
        public static List<Cell> FindPath(Cell start, Cell goal, float sphereRadius)
        {
            var openSet = new SimplePriorityQueue<Cell>();
            openSet.Enqueue(start, 0);

            var cameFrom = new Dictionary<Cell, Cell>();

            var gScore = new Dictionary<Cell, float>
            {
                [start] = 0f
            };

            var fScore = new Dictionary<Cell, float>
            {
                [start] = Heuristic(start, goal, sphereRadius)
            };

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();

                if (current == goal)
                    return ReconstructPath(cameFrom, current);

                foreach (var neighbor in current.Neighbors)
                {
                    // skip blocked
                    if (neighbor.IsWater)
                        continue;

                    float tentativeG = gScore[current] + Cost(current, neighbor);

                    if (!gScore.ContainsKey(neighbor) || tentativeG < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeG;

                        float f = tentativeG + Heuristic(neighbor, goal, sphereRadius);
                        fScore[neighbor] = f;

                        openSet.Enqueue(neighbor, f);
                    }
                }
            }

            return null; // no path
        }
        
        static float Cost(Cell a, Cell b)
        {
            float distance = (a.Center - b.Center).Magnitude;
            return distance;
        }
        
        static float Heuristic(Cell a, Cell b, float radius)
        {
            var na = a.Center.Normalized;
            var nb = b.Center.Normalized;

            float dot = Vector3Data.Dot(na, nb);
            dot = Math.Clamp(dot, -1f, 1f);

            float angle = (float)Math.Acos(dot);
            return angle * radius;
        }
        
        public static void HighlightCell(Cell cell)
        {
            cell.Color = new ColorData(1, 0, 0, 0);
        }
        
        static List<Cell> ReconstructPath(Dictionary<Cell, Cell> cameFrom, Cell current)
        {
            var path = new List<Cell> { current };

            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Add(current);
            }

            path.Reverse();
            return path;
        }
        
        public class SimplePriorityQueue<T>
        {
            private List<(T item, float priority)> elements = new();

            public int Count => elements.Count;

            public void Enqueue(T item, float priority)
            {
                elements.Add((item, priority));
            }

            public T Dequeue()
            {
                int bestIndex = 0;

                for (int i = 1; i < elements.Count; i++)
                {
                    if (elements[i].priority < elements[bestIndex].priority)
                        bestIndex = i;
                }

                T bestItem = elements[bestIndex].item;
                elements.RemoveAt(bestIndex);
                return bestItem;
            }
        }
    }
}