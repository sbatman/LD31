using System;

namespace LD31.Math
{
    /// <summary>
    /// This struct represents a two dimensional vector.
    /// </summary>
    public struct Vector3
    {
        /// <summary>
        /// backing field
        /// </summary>
        private Double x;

        /// <summary>
        /// backing field
        /// </summary>
        private Double y;

        /// <summary>
        /// backing field
        /// </summary>
        private Double z;

        /// <summary>
        /// X value
        /// </summary>
        public Double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        /// <summary>
        /// Y value
        /// </summary>
        public Double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        /// <summary>
        /// Z value
        /// </summary>
        public Double Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }

        /// <summary>
        /// Secondary CTOR
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector3(Double x, Double y, Double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Teritiary Ctor
        /// </summary>
        /// <param name="xyz"></param>
        public Vector3(Double xyz)
        {
            this.x = xyz;
            this.y = xyz;
            this.z = xyz;
        }
    }
}
