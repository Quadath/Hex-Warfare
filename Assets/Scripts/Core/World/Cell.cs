using System;
using System.Collections.Generic;
using Core.Structs;

namespace Core
{
    public class Cell
    {
        //Messy
        public readonly Vector3Data Center;
        public List<Vector3Data> Corners { get; }
        public List<Cell> Neighbors { get; } = new List<Cell>();
        public List<Sector> Sectors { get; } = new List<Sector>();

        
        public int OccupiedBy { get; internal set; } = 0; 
        public bool IsWater {get; internal set; }
        
        private bool Unleashed { get; set; } = false;
        private event Action<Entity> EntityEntered;
        private event Action<Entity> EntityExited;
        
        

        internal readonly int ID;
        private readonly int _totalCells;
        private readonly int[] _visited = new int[700];
        private int _visitVersion;
        //DEBUG
        private bool isHighlighted;
        
        public ColorData Color
        {
            get
            {
                if (isHighlighted) return Constants.HighlightedColor;
                if (!Unleashed) return Constants.HiddenCell;
                var substanceCol = IsWater ? Constants.WaterColor : Constants.GroundColor;
                var playerCol = Constants.PlayerColors[OccupiedBy];
                return ColorData.Lerp(substanceCol, playerCol, .15f);
            }
        }
        
        public void Occupy(int player)  { //make internal later
            OccupiedBy = player;
            Unleashed = true;
            foreach (var n in Neighbors)
            {
                n.Unleash();
            }
        }
        public void Highlight() => isHighlighted = true;
        
        internal Cell(Vector3Data center, List<Vector3Data> corners, int id) 
        {
            Center = center;
            Corners = corners;
            for (int c = 1; c < corners.Count; c++)
            {
                Sectors.Add(new Sector(this, corners[c - 1], corners[c]));
            }
            Sectors.Add(new Sector(this, corners[Corners.Count - 1], corners[0]));
            ID = id;
        }

        internal void Enter(Entity entity)
        {
            DebugUtils.Message(this, "Cell " + ID + " has been entered");
            EntityEntered?.Invoke(entity);
        }

        internal void Exit(Entity entity)
        {
            DebugUtils.Message(this, "Cell " + ID + " has been exited");
            EntityExited?.Invoke(entity);
        }
        internal void AddEntityEnteredListener(Action<Entity> action) => EntityEntered += action;
        internal void AddEntityExitedListener(Action<Entity> action) => EntityExited += action;
        internal void RemoveEntityEnteredListener(Action<Entity> action) => EntityEntered -= action;
        internal void RemoveEntityExitedListener(Action<Entity> action) => EntityExited -= action;
        
        private void Unleash() => Unleashed = true;
        
        public List<Cell> GetCellsInRange(Cell start, int depth)
        {
            _visitVersion++;

            var result = new List<Cell>();
            var frontier = new List<Cell> { start };

            _visited[start.ID] = _visitVersion;

            for (int d = 0; d < depth; d++)
            {
                var nextFrontier = new List<Cell>();
                foreach (var cell in frontier)
                {
                    foreach (var neighbor in cell.Neighbors)
                    {
                        // Already visited during THIS search?
                        if (_visited[neighbor.ID] == _visitVersion)
                            continue;

                        // Mark as visited
                        _visited[neighbor.ID] = _visitVersion;

                        result.Add(neighbor);
                        nextFrontier.Add(neighbor);
                    }
                }
                frontier = nextFrontier;
            }

            return result;
        }

        public class Sector
        {
            private Cell Parent { get; set; }
            internal Entity Building { get; private set; } = null;
            public ColorData Color => Parent.Color * .95f;
            public Vector3Data Center {get; private set; }
            

            internal Sector(Cell parent, Vector3Data p1, Vector3Data p2)
            {
                Parent =  parent;
                Center = (parent.Center + p1 + p2) / 3;
            }
            
            internal void SetBuilding(Entity building) => Building = building;
            
            public void Highlight()
            {
               // Color = Constants.HighlightedColor;
            }
        }
        
        public Sector GetClosestSector(Vector3Data point)
        {
            Sector sector = null;
            float closest = float.MaxValue;

            foreach (Sector s in Sectors)
            {
                float sqrDist = (s.Center - point).SqrMagnitude;
                if (sqrDist < closest)
                {
                    sector = s;
                    closest = sqrDist;
                }
            }
            return sector;
        }
    }
}