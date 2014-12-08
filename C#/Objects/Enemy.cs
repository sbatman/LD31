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
        protected readonly Vector3 _Scale = new Vector3(8, 24, 8);

        /// <summary>
        /// The player this enemy targets.
        /// </summary>
        protected readonly Player _Target;

        /// <summary>
        /// How fast the enemy can move.
        /// </summary>
        protected readonly Double _MovementSpeed = 0.1;

        protected readonly Vector3 _DrawOffset = new Vector3(0, 0, 10);

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
            GraphicsManager.DrawVoxel(Position - _DrawOffset, Colour.Green, _Scale);
        }

        /// <summary>
        /// Enemy update logic.
        /// </summary>
        public override void Update(Double msSinceLastUpdate)
        {
            //// heroPos is the position of the hero.
            Vector3 direction = Position - _Target.Position;

            if (direction.Z < -5 && IsOnFloor() && _JumpCoolDown <= 0)
            {
                Velocity.Z += 2;
                _JumpCoolDown = 120;
            }

            direction.Z = 0;
            ////assuming here that velocity is a length and not a vector.
            Velocity.X += (direction.X > 0 ? -_MovementSpeed : _MovementSpeed) * (msSinceLastUpdate / 16); ;
            Velocity.Y += (direction.Y > 0 ? -_MovementSpeed : _MovementSpeed) * (msSinceLastUpdate / 16); ;

            base.Update(msSinceLastUpdate);
        }

        public override void Kill()
        {
            base.Kill();
        }
    }
}
