using LD31.Graphics;
using LD31.Math;
using System;

namespace LD31.Objects
{
    /// <summary>
    /// This class represents an enemy for the player.
    /// </summary>
    public class Enemy : Combatant
    {
        /// <summary>
        /// The size of the enemy
        /// </summary>
        protected Vector3 _Scale = new Vector3(40);

        /// <summary>
        /// The player this enemy targets.
        /// </summary>
        protected Player _Target;

        /// <summary>
        /// How fast the enemy can move.
        /// </summary>
        protected Double _MovementSpeed = 0.005;

         /// <summary>
        /// CTOR
        /// </summary>
        public Enemy(Vector3 position, Player target)
            : base(position)
        {
            _Target = target;
        }


        /// <summary>
        /// Make sure we can draw projectiles.
        /// </summary>
        public override void Draw()
        {
            GraphicsManager.DrawVoxel(Position, Colour.Green, _Scale);
        }

        /// <summary>
        /// Enemy update logic.
        /// </summary>
        public override void Update(Double msSinceLastUpdate)
        {
            //// heroPos is the position of the hero.
            Vector3 direction = Position - _Target.Position;

            ////assuming here that velocity is a length and not a vector.
            Position -= direction * _MovementSpeed;
        }

        public override void Kill()
        {
            base.Kill();
        }
    }
}
