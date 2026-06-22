using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Cell
    {
        public Vector3Data Center;
        public ColorData Color;
        public bool IsWater;
        public List<Vector3Data> Corners = new List<Vector3Data>();
        public List<Cell> Neighbors = new List<Cell>();
    }
}