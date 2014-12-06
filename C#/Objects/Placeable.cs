using LD31.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Objects
{
    /// <summary>
    /// This class represents a game object that has a position in the game world.
    /// </summary>
    public abstract class Placeable : GameObject
    {
        /// <summary>
        /// Private backing field for position
        /// </summary>
        protected Vector2 position = new Vector2();

        /// <summary>
        /// This value represents the position of the object.
        /// </summary>
        Vector2 Position
        {
            get
            {
                return position;
            }
        }


         /// <summary>
        /// CTOR
        /// </summary>
        public Placeable(Vector2 position)
        {
            this.position = position;
        }
    }
}
