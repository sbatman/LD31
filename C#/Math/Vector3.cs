using System;

namespace LD31.Math
{
    /// <summary>
    ///     This struct represents a two dimensional vector.
    /// </summary>
    public class Vector3 : ICloneable
    {
        /// <summary>
        ///     backing field
        /// </summary>
        private Double _Z;

        /// <summary>
        ///     Secondary CTOR
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(Double x, Double y, Double z)
        {
            X = x;
            Y = y;
            _Z = z;
        }

        /// <summary>
        ///     Teritiary Ctor
        /// </summary>
        /// <param name="xyz"></param>
        public Vector3(Double xyz)
        {
            X = xyz;
            Y = xyz;
            _Z = xyz;
        }

        /// <summary>
        ///     X value
        /// </summary>
        public Double X { get; set; }

        /// <summary>
        ///     Y value
        /// </summary>
        public Double Y { get; set; }

        /// <summary>
        ///     Z value
        /// </summary>
        public Double Z
        {
            get { return _Z; }
            set { _Z = value; }
        }

        public Vector2 XY
        {
            get { return new Vector2(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Vector2 XZ
        {
            get { return new Vector2(X, _Z); }
            set
            {
                X = value.X;
                _Z = value.Y;
            }
        }

        public Vector2 YZ
        {
            get { return new Vector2(Y, _Z); }
            set
            {
                Y = value.X;
                _Z = value.Y;
            }
        }

        public Vector2 YX
        {
            get { return new Vector2(Y, X); }
            set
            {
                Y = value.X;
                X = value.Y;
            }
        }

        public Vector2 ZX
        {
            get { return new Vector2(_Z, X); }
            set
            {
                _Z = value.X;
                X = value.Y;
            }
        }

        public Vector2 ZY
        {
            get { return new Vector2(_Z, Y); }
            set
            {
                _Z = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        ///     Allow people to make copies of this vector.
        /// </summary>
        /// <returns></returns>
        public Object Clone()
        {
            return new Vector3(X, Y, Z);
        }

        /// <summary>
        ///     Overload of '+' operator
        /// </summary>
        public static Vector3 operator +(Vector3 c1, Vector3 c2)
        {
            return new Vector3(c1.X + c2.X, c1.Y + c2.Y, c1.Z + c2.Z);
        }

        /// <summary>
        ///     Overload of '-' operator
        /// </summary>
        public static Vector3 operator -(Vector3 c1, Vector3 c2)
        {
            return new Vector3(c1.X - c2.X, c1.Y - c2.Y, c1.Z - c2.Z);
        }

        /// <summary>
        ///     Overload of '*' operator
        /// </summary>
        public static Vector3 operator *(Vector3 c1, double c2)
        {
            return new Vector3(c1.X*c2, c1.Y*c2, c1.Z*c2);
        }

        /// <summary>
        ///     Overload of '*' operator
        /// </summary>
        public static Vector3 operator *(Vector3 c1, Vector3 c2)
        {
            return new Vector3(c1.X*c2.X, c1.Y*c2.Y, c1.Z*c2.Z);
        }

        public static Double DistanceSquared(Vector3 c1, Vector3 c2)
        {
            return System.Math.Pow(c1.X - c2.X, 2) + System.Math.Pow(c1.Y - c2.Y, 2) + System.Math.Pow(c1.Z - c2.Z, 2);
        }

        public static Vector3 Normalize(Vector3 vec)
        {
            double magnitude = System.Math.Sqrt((vec.X*vec.X) + (vec.Y*vec.Y) + (vec.Z*vec.Z));
            return new Vector3(vec.X/magnitude, vec.Y/magnitude, vec.Z/magnitude);
        }
    }
}