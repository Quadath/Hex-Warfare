namespace Core.Structs
{
    public struct ColorData
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public ColorData(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static ColorData operator *(ColorData a, float m)
        {
            return new ColorData(a.R * m, a.G * m, a.B * m, a.A * m);
        }

        public static ColorData Lerp(ColorData a, ColorData b, float t)
        {
            return new ColorData(a.R + (b.R - a.R) * t,
                a.G + (b.G - a.G) * t,
                a.B + (b.B - a.B) * t,
                a.A + (b.A - a.A) * t);
        }
    }
}