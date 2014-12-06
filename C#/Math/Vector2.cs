using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Math
{
    /// <summary>
    /// This struct represents a two dimensional vector.
    /// </summary>
    public struct Vector2
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
        /// Primary CTOR
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(Double x, Double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Secondary Ctor
        /// </summary>
        /// <param name="xAndY"></param>
        public Vector2(Double xAndY)
        {
            this.x = xAndY;
            this.y = xAndY;
        }
    }
}
