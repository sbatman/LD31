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
        private double x;

        private double y;

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
    

        public Vector2(Double x, Double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(Double xAndY)
        {
            this.x = xAndY;
            this.y = xAndY;
        }
    }
}
