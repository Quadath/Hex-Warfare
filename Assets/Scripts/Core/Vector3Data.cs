using System;

namespace Core
{
    public struct Vector3Data
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3Data(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public float Magnitude =>
            (float)Math.Sqrt(X * X + Y * Y + Z * Z);

        public Vector3Data Normalized
        {
            get
            {
                float mag = Magnitude;
                if (mag == 0f)
                    return new Vector3Data();

                return new Vector3Data(X / mag, Y / mag, Z / mag);
            }
        }
        
        public void Normalize()
        {
            float mag = Magnitude;
            if (mag == 0f) return;

            X /= mag;
            Y /= mag;
            Z /= mag;
        }
        
        public static Vector3Data operator +(Vector3Data a, Vector3Data b)
            => new Vector3Data(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Vector3Data operator -(Vector3Data a, Vector3Data b)
            => new Vector3Data(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Vector3Data operator *(Vector3Data a, float x)
            => new Vector3Data(a.X * x, a.Y * x, a.Z * x);
        public static Vector3Data operator /(Vector3Data a, float x)
            => new Vector3Data(a.X / x, a.Y / x, a.Z / x);
    }
}