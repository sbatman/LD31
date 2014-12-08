using System;
using System.Collections.Generic;
using LD31.Graphics;
using LD31.Math;

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

        public static readonly  List<Vector3> SpawnLocations = new List<Vector3>();

        private static readonly Random _RND = new Random();

        /// <summary>
        /// CTOR
        /// </summary>
        public Enemy(Vector3 position, Player target)
            : base(position)
        {
            if (SpawnLocations.Count <= 0)
            {
                Dispose();
                return;
                ;
            }
            int randomPosition = _RND.Next(0, SpawnLocations.Count);
            Position = SpawnLocations[randomPosition];

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
            if (Disposed) return;
            //// heroPos is the position of the hero.
            Vector3 direction = Position - _Target.Position;

            if (direction.Z < -5 && IsOnFloor() && _JumpCoolDown <= 0)
            {
                Velocity.Z += 6;
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
