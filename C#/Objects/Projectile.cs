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

        public Projectile(Vector3 position)
            : base(position)
        {

        }

        public override void Update(double msSinceLastUpdate)
        {

            Velocity = new Vector3(0);
            if (Velocity.Z <= 0)
            {
                Velocity.Z = 0;
            }
            

            //float xRadius = Velocity.X > 0 ? _CollisionRadius : -_CollisionRadius;
            //float yRadius = Velocity.Y > 0 ? _CollisionRadius : -_CollisionRadius;


            Position += Velocity;
        }

        /// <summary>
        /// allow all moveable types to implement their own draw calls.
        /// </summary>
        public override void Draw()
        {
            GraphicsManager.DrawVoxel(Position, _Colour, _Scale);
        }
    }
}
