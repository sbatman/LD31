using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LD31.Math;
using LD31.Objects;
using LD31.World;

namespace LD31.Graphics
{
    public class Explosion : GameObject
    {
        public class Particle
        {
            public Vector3 Position;
            public Colour Colour;
            public Double Size;
            public Double Life;
            public Vector3 Velocity;
        }

        List<Particle> _ActiveParticles = new List<Particle>();
        private Vector3 _Positon;
        private int _ParticleToProduce;
        private Colour _Colour;
        private Double _Strength;
        private static Random _RND = new Random();

        public Explosion(Colour colour, Vector3 position, Double strength)
        {
            _Colour = colour;
            _Positon = position;
            _Strength = strength;
            _ParticleToProduce = (int)(220 * strength);
        }

        public void CreateParticle()
        {
            Particle p = new Particle()
            {
                Colour = _Colour,
                Life = _RND.Next(80, 100) / 100.0,
                Position = _Positon,
                Size = _RND.Next(1, 2 + (int)(4 * _Strength)),
                Velocity = new Vector3(_RND.Next(-30, 30) / 10.0, _RND.Next(-30, 30) / 10.0, _RND.Next(-30, 30) / 10.0),
            };
            _ActiveParticles.Add(p);
        }

        public Vector3 Positon
        {
            get { return _Positon; }
            set { _Positon = value; }
        }

        public override void Draw()
        {
            foreach (Particle p in new List<Particle>(_ActiveParticles).Where(a => a.Life >= 0))
            {
                GraphicsManager.DrawVoxel(p.Position, p.Colour, new Vector3(p.Size));
            }
        }


        public override void Update(double msSinceLastUpdate)
        {
            while (_ParticleToProduce > 0)
            {
                _ParticleToProduce--;
                CreateParticle();
            }

            if (!_ActiveParticles.Any(a => a.Life > 0))
            {
                Dispose();
                return;
            }

            foreach (Particle p in new List<Particle>(_ActiveParticles))
            {
                p.Life -= 0.01f * (msSinceLastUpdate / 16); ;
                if (p.Life <= 0)
                {
                    _ActiveParticles.Remove(p);
                    continue;
                }
                p.Colour.A = (byte)System.Math.Min(255, 512 * p.Life);

                if (!Game.CurrentLevel.IsSolid(p.Position.X, p.Position.Y, p.Position.Z - p.Size))
                {
                    p.Velocity.Z -= Level.GRAVITY * (msSinceLastUpdate / 1000);
                }
                else
                {
                    p.Velocity *= 0.9;
                    if (p.Velocity.Z <= 0)
                    {
                        p.Velocity.Z = 0;
                    }
                }

                if (p.Velocity.Z > 0 && Game.CurrentLevel.IsSolid(p.Position.X, p.Position.Y, p.Position.Z + 10))
                {
                    p.Velocity.Z = 0;
                }

                Double xRadius = p.Velocity.X > 0 ? p.Size : -p.Size;
                Double yRadius = p.Velocity.Y > 0 ? p.Size : -p.Size;

                if (Game.CurrentLevel.IsSolid(p.Position.X + p.Velocity.X + xRadius, p.Position.Y, p.Position.Z))
                {
                    p.Velocity.X = 0;
                }

                if (Game.CurrentLevel.IsSolid(p.Position.X, p.Position.Y + p.Velocity.Y + yRadius, p.Position.Z))
                {
                    p.Velocity.Y = 0;
                }

                p.Position += p.Velocity * (msSinceLastUpdate / 16); ;
            }
        }
    }
}
