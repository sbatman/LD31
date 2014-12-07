using System;

namespace LD31.Math
{
    /// <summary>
    /// This struct represents a two dimensional vector.
    /// </summary>
    public class Vector2
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
        /// Secondary CTOR
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(Double x, Double y)
        {
            _X = x;
            _Y = y;
        }

        /// <summary>
        /// Teritiary Ctor
        /// </summary>
        /// <param name="xAndY"></param>
        public Vector2(Double xAndY)
        {
            _X = xAndY;
            _Y = xAndY;
        }

        public static Vector2 Rotate(Vector2 vec, Double degrees)
        {
            double angle = (degrees * System.Math.PI) / 180.0f;
            return new Vector2(0)
            {
                X = vec.X * System.Math.Cos(angle) - vec.Y * System.Math.Sin(angle),
                Y = vec.X * System.Math.Sin(angle) + vec.Y * System.Math.Cos(angle)
            };
        }
    }
}
