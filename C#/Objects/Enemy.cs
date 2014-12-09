using System;
using System.Collections.Generic;
using LD31.Graphics;
using LD31.Math;

namespace LD31.Objects
{
    /// <summary>
    ///     This class represents an enemy for the player.
    /// </summary>
    public class Enemy : Combatant
    {
        /// <summary>
        ///     How fast the enemy can move.
        /// </summary>
        protected const Double MOVEMENT_SPEED = 0.1;

        public static readonly List<Vector3> SpawnLocations = new List<Vector3>();
        private static readonly Random _RND = new Random();
        protected readonly Vector3 _DrawOffset = new Vector3(0, 0, 10);

        /// <summary>
        ///     The size of the enemy
        /// </summary>
        protected readonly Vector3 _Scale = new Vector3(8, 24, 8);

        /// <summary>
        ///     The player this enemy targets.
        /// </summary>
        protected readonly Player _Target;

        /// <summary>
        ///     CTOR
        /// </summary>
        public Enemy(Vector3 position, Player target)
            : base(position)
        {
            if (SpawnLocations.Count <= 0)
            {
                Dispose();
                return;
            }
            int randomPosition = _RND.Next(0, SpawnLocations.Count);
            Position = SpawnLocations[randomPosition];

            _Target = target;
        }

        /// <summary>
        ///     Make sure we can draw projectiles.
        /// </summary>
        public override void Draw()
        {
            GraphicsManager.DrawVoxel(Position - _DrawOffset, Colour.Green, _Scale);
        }

        /// <summary>
        ///     Enemy update logic.
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
            Velocity.X += (direction.X > 0 ? -MOVEMENT_SPEED : MOVEMENT_SPEED)*(msSinceLastUpdate/16);
            Velocity.Y += (direction.Y > 0 ? -MOVEMENT_SPEED : MOVEMENT_SPEED)*(msSinceLastUpdate/16);

            base.Update(msSinceLastUpdate);
        }
    }
}