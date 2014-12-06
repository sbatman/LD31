using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Objects
{
    /// <summary>
    /// This struct represents a two dimensional vector.
    /// </summary>
    struct Vector2
    {
        public Double X { get; set; }

        public Double Y { get; set; }

        public Vector2(Double x, Double y)
        {
            X = x;
            Y = y;
        }

        public Vector2(Double xAndY)
        {
            X = xAndY;
            Y = xAndY;
        }
    }
}
