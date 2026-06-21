using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core;

namespace Systems
{
    public static class Vector3Extensions
    {
        public static Vector3 ToUnity(Vector3Data v) =>
            new Vector3(v.X, v.Y, v.Z);
        public static Vector3Data ToCore(Vector3 v) =>
            new Vector3Data(v.x, v.y, v.z);
        
        public static List<Vector3> ToUnity(this IEnumerable<Vector3Data> source)
            => source.Select(ToUnity).ToList();

        public static List<Vector3Data> ToCore(this IEnumerable<Vector3> source)
            => source.Select(ToCore).ToList();
    }
}