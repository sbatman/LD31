using LD31.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Objects
{
    /// <summary>
    /// This class represents an enemy for the player.
    /// </summary>
    public class Enemy : Combatant
    {
         /// <summary>
        /// CTOR
        /// </summary>
        public Enemy(Vector2 position)
            : base(position)
        {
        }
    }
}
