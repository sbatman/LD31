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
        protected Vector3 _Scale = new Vector3(4);
        protected DateTime _CreationTime;
        protected TimeSpan _MaxLifeSpan = TimeSpan.FromMinutes(1);
        protected Boolean _Alive = true;
        protected Double _RotationZ;
        protected Colour _Colour;

        protected Int32 _CollisionHeight = 30;
        protected Int32 _CollisionRadius = 8;

        /// <summary>
        /// This boolean states if the projectile is dead or not.
        /// </summary>
        public override Boolean Alive
        {
            get { return _Alive; }
        }

        public Projectile(Vector3 position, Double rotationZ, Colour colour)
            : base(position)
        {
            _CreationTime = DateTime.Now;
            _RotationZ = rotationZ;
            _Colour = colour;
        }

        public override void Update(double msSinceLastUpdate)
        {
            Vector2 movement = Vector2.Rotate(new Vector2(0, -0.2), _RotationZ);
            Velocity.X += movement.X;
            Velocity.Y += movement.Y;

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
            if(DateTime.Now  - _CreationTime > _MaxLifeSpan)
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
    }
}
