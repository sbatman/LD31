using LD31.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Objects
{
    /// <summary>
    /// This class represents a game object that can die.
    /// </summary>
    public abstract class Mortal : Moveable
    {
        /// <summary>
        /// backing field
        /// </summary>
        protected Int32 health = 100;

        /// <summary>
        /// This value represents the health of the player. Starts at 100, player dies at 0.
        /// </summary>
        public Int32 Health
        {
            get
            {
                return health;
            }
        }

        /// <summary>
        /// This boolean states if the player is dead or not.
        /// </summary>
        Boolean IsDead
        {
            get
            {
                return health <= 0;
            }
        }


         /// <summary>
        /// CTOR
        /// </summary>
        public Mortal(Vector2 position)
            : base(position)
        {
        }


        /// <summary>
        /// This function damages the mortal.
        /// </summary>
        /// <param name="damage"></param>
        public void Damage(Int32 damage)
        {
            if (damage > 0) health -= damage;
        }


        /// <summary>
        /// This function heals the mortal.
        /// </summary>
        /// <param name="healAmount"></param>
        public void Heal(Int32 healAmount)
        {
            if (healAmount > 0) health += healAmount;
        }
    }
}
