using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Objects
{
    /// <summary>
    /// This class represents the player
    /// </summary>
    public class Player : GameObject
    {
        /// <summary>
        /// The default health value of the player.
        /// </summary>
        private Int32 startingHealth = 100;

        /// <summary>
        /// This value represents the health of the player. Starts at 100, player dies at 0.
        /// </summary>
        Int32 Health { get; set; }

        /// <summary>
        /// This value represents the position of the player.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// CTOR
        /// </summary>
        public Player(Vector2 position)
        {
            Health = startingHealth;
        }
    }
}
