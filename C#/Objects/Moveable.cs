using LD31.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Objects
{
    /// <summary>
    /// This class represents a game object that can be moved.
    /// </summary>
    public abstract class Moveable : Placeable
    {
        /// <summary>
        /// This function moves the object.
        /// </summary>
        /// <param name="movement"></param>
        public void Move(Vector2 movement)
        {
            position.X += movement.X;
            position.Y += movement.Y;
        }

         /// <summary>
        /// CTOR
        /// </summary>
        public Moveable(Vector2 position)
            : base(position)
        {
        }
    }
}
