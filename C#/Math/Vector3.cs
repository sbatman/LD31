using System;

namespace LD31.Math
{
    /// <summary>
    /// This struct represents a two dimensional vector.
    /// </summary>
    public class Vector3
    {
        /// <summary>
        /// backing field
        /// </summary>
        private Double _X;

        /// <summary>
        /// backing field
        /// </summary>
        private Double _Y;

        /// <summary>
        /// backing field
        /// </summary>
        private Double _Z;

        /// <summary>
        /// X value
        /// </summary>
        public Double X
        {
            get
            {
                return _X;
            }
            set
            {
                _X = value;
            }
        }

        /// <summary>
        /// Y value
        /// </summary>
        public Double Y
        {
            get
            {
                return _Y;
            }
            set
            {
                _Y = value;
            }
        }

        /// <summary>
        /// Z value
        /// </summary>
        public Double Z
        {
            get
            {
                return _Z;
            }
            set
            {
                _Z = value;
            }
        }

        /// <summary>
        /// Secondary CTOR
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector3(Double x, Double y, Double z)
        {
            _X = x;
            _Y = y;
            _Z = z;
        }

        /// <summary>
        /// Teritiary Ctor
        /// </summary>
        /// <param name="xyz"></param>
        public Vector3(Double xyz)
        {
            _X = xyz;
            _Y = xyz;
            _Z = xyz;
        }

        public static Vector3 operator +(Vector3 c1, Vector3 c2)
        {
            return new Vector3(c1.X + c2.X, c1.Y + c2.Y, c1.Z + c2.Z);
        }
    }
}
