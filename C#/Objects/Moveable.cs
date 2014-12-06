using System;
using LD31.Math;

namespace LD31.Objects
{
    /// <summary>
    /// This class represents a game object that can be moved.
    /// </summary>
    public abstract class Moveable : GameObject
    {
        /// <summary>
        /// Private backing field for position
        /// </summary>
        private Vector2 _Position;

        /// <summary>
        /// This value represents the position of the object.
        /// </summary>
        Vector2 Position
        {
            get
            {
                return _Position;
            }
        }

        /// <summary>
        /// backing field
        /// </summary>
        private Int32 _Health = 100;

        /// <summary>
        /// This value represents the health of the player. Starts at 100, player dies at 0.
        /// </summary>
        public Int32 Health
        {
            get
            {
                return _Health;
            }
        }

        /// <summary>
        /// This boolean states if the player is dead or not.
        /// </summary>
        private Boolean IsDead
        {
            get
            {
                return _Health <= 0;
            }
        }

        /// <summary>
        /// This function moves the object.
        /// </summary>
        /// <param name="movement"></param>
        public void Move(Vector2 movement)
        {
            _Position.X += movement.X;
            _Position.Y += movement.Y;
        }

        /// <summary>
        /// CTOR
        /// </summary>
        public Moveable(Vector2 position)
        {
            _Position = position;
        }

        /// <summary>
        /// This function damages the mortal.
        /// </summary>
        /// <param name="damage"></param>
        public void Damage(Int32 damage)
        {
            if (damage > 0) _Health -= damage;
        }

        /// <summary>
        /// This function heals the mortal.
        /// </summary>
        /// <param name="healAmount"></param>
        public void Heal(Int32 healAmount)
        {
            if (healAmount > 0) _Health += healAmount;
        }
    }
}
