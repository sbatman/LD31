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
        public Int32 X { get; set; }

        public Int32 Y { get; set; }

        public Vector2(Int32 x, Int32 y)
        {
            X = x;
            Y = y;
        }

        public Vector2(Int32 xAndY)
        {
            X = xAndY;
            Y = xAndY;
        }
    }
}
