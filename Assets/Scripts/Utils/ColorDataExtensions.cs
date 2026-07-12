using UnityEngine;
using Core.Structs;

namespace Utils
{
    public static class ColorDataExtensions
    {
        public static Color ToUnity(ColorData c) =>
            new Color(c.R, c.G, c.B, c.A);
        public static ColorData ToCore(Color c) =>
            new ColorData(c.r, c.g, c.b, c.a);
    }
}