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
        private Double _X;
        private Double _Y;

        public Double X
        {
            get { return _X;}
            set { _X = value; }
        }

        public Double Y
        {
            get { return _Y; }
            set { _Y = value; }
        }

        public Vector2(Double x, Double y)
        {
           _X = x;
           _Y = y;
        }

        public Vector2(Double xAndY)
        {
            _X = xAndY;
            _Y = xAndY;
        }
    }
}
