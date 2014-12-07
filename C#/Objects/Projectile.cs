using LD31.Graphics;
using LD31.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Objects
{
    public class Projectile : Moveable
    {
        protected Int32 _Size = 2;
        protected Vector3 _Scale;
        protected DateTime _CreationTime;
        protected TimeSpan _MaxLifeSpan = TimeSpan.FromMinutes(1);
        protected Vector3 _Velocity;
        protected Colour _Colour;
        protected Weapon _Owner;

        protected Int32 _CollisionRadius = 4;

        public int Size
        {
            get { return _Size; }
        }

        public Double Damage
        {
            get { return _Owner.DamagePerShot; }
        }

        public Projectile(Weapon owner, Vector3 position, Vector3 velocity, Colour colour)
            : base(position)
        {
            _Owner = owner;
            _CreationTime = DateTime.Now;
            _Velocity = velocity;
            _Colour = colour;
            _Scale = new Vector3(_Size);
        }

        public override void Update(double msSinceLastUpdate)
        {
            Velocity.X += _Velocity.X;
            Velocity.Y += _Velocity.Y;
            Velocity.Z += _Velocity.Z;

            Position += Velocity;

            ////Make sure all projectiles die after they hit things/walls.
            float xRadius = Velocity.X > 0 ? _CollisionRadius : -_CollisionRadius;
            float yRadius = Velocity.Y > 0 ? _CollisionRadius : -_CollisionRadius;

            if (Game.CurrentLevel.IsSolid(Position.X + Velocity.X + xRadius, Position.Y, Position.Z))
            {
               Dispose();
            }

            if (Game.CurrentLevel.IsSolid(Position.X, Position.Y + Velocity.Y + yRadius, Position.Z))
            {
                Dispose();
            }

            //Kill all projectiles if they live longer than their max lifespan, this probably means they somehow escaped the bounds of the world
            if (DateTime.Now - _CreationTime > _MaxLifeSpan)
            {
                Dispose();
            }
        }

        /// <summary>
        /// Make sure we can draw projectiles.
        /// </summary>
        public override void Draw()
        {
            GraphicsManager.DrawVoxel(Position, _Colour, _Scale);
        }

        public override void Dispose()
        {
            base.Dispose();
            _Owner = null;
           
        }

        public void AttemptToHit(Combatant combatant)
        {
            if (Disposed) return;
            if (combatant == _Owner.Owner) return;
            combatant.AttemptToHit(this);
        }
    }
}
