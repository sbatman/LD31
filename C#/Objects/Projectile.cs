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
        protected Vector3 _Scale = new Vector3(2);
        protected DateTime _CreationTime;
        protected TimeSpan _MaxLifeSpan = TimeSpan.FromMinutes(1);
        protected Boolean _Alive = true;
        protected Vector3 _Velocity;
        protected Colour _Colour;
        protected Weapon _Owner;

        protected Int32 _CollisionRadius = 8;

        /// <summary>
        /// This boolean states if the projectile is dead or not.
        /// </summary>
        public override Boolean Alive
        {
            get { return _Alive; }
        }

        public Projectile(Weapon owner, Vector3 position, Vector3 velocity, Colour colour)
            : base(position)
        {
            _Owner = owner;
            _CreationTime = DateTime.Now;
            _Velocity = velocity;
            _Colour = colour;
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
                this._Alive = false;
            }

            if (Game.CurrentLevel.IsSolid(Position.X, Position.Y + Velocity.Y + yRadius, Position.Z))
            {
                this._Alive = false;
            }

            //Kill all projectiles if they live longer than their max lifespan, this probably means they somehow escaped the bounds of the world
            if (DateTime.Now - _CreationTime > _MaxLifeSpan)
            {
                this._Alive = false;
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
            _Owner = null;
            base.Dispose();
        }
    }
}
