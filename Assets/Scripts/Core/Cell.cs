using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Cell
    {
        public Vector3Data _center;
        public Vector3 center;
        public ColorData color;
        public bool isWater;
        public List<Vector3> corners = new List<Vector3>();
        public List<Vector3Data> _corners = new List<Vector3Data>();
        public List<int> neighbors = new List<int>();
    }
}