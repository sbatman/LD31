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
        private Colour _Colour = new Colour(255, 255);
        private Vector3 _Scale = new Vector3(4);
        private DateTime _CreationTime;
        private TimeSpan _LifeSpan = TimeSpan.FromSeconds(2);

        public Projectile(Vector3 position)
            : base(position)
        {
            _CreationTime = DateTime.Now;
        }

        public override void Update(double msSinceLastUpdate)
        {
            Vector2 movement = Vector2.Rotate(new Vector2(0, -0.2), Graphics.GraphicsManager.GetCamera().RotationZ);
            Velocity.X += movement.X;
            Velocity.Y += movement.Y;

            Position += Velocity;

            //Make sure all projectiles die after a given period of time.
            if(DateTime.Now - _CreationTime > _LifeSpan)
            {
                this.Dispose();
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
