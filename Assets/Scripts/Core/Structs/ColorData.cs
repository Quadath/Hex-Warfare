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
    }
}