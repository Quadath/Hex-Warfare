using System;

namespace Shared
{
    public struct Vector3Data
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3Data(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public float Magnitude =>
            (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        public float SqrMagnitude =>
            X * X + Y * Y + Z * Z;

        public Vector3Data Normalized
        {
            get
            {
                float sqrMag = SqrMagnitude;
                if (sqrMag == 0f)
                    return new Vector3Data(0, 0, 0);

                float invMag = 1f / (float)Math.Sqrt(sqrMag);
                return new Vector3Data(X * invMag, Y * invMag, Z * invMag);
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

        public void Translate(Vector3Data translation)
        {
            X += translation.X;
            Y += translation.Y;
            Z += translation.Z;
        }
        
        public static float Dot(Vector3Data a, Vector3Data b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }
        
        public static Vector3Data Cross(Vector3Data a, Vector3Data b)
        {
            return new Vector3Data(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }
        
        public static Vector3Data Right => new Vector3Data(1, 0, 0);
        public static Vector3Data Up => new Vector3Data(0, 1, 0);
        public static Vector3Data Forward => new Vector3Data(0, 0, 1);
        
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