using System;
using System.Collections.Generic;
using System.Linq;
using LD31.Graphics;
using LD31.Math;
using LD31.World;

namespace LD31.Objects
{
    public abstract class Combatant : Moveable
    {
        protected const Int32 COLLISION_HEIGHT = 32;
        protected const Int32 COLLISION_RADIUS = 8;

        /// <summary>
        ///     backing field
        /// </summary>
        protected Weapon _CurrentWeapon;

        /// <summary>
        ///     backing field
        /// </summary>
        private Double _Health = 100;

        protected Double _JumpCoolDown;

        /// <summary>
        ///     backing field
        /// </summary>
        protected readonly HashSet<Weapon> _CurrentWeapons = new HashSet<Weapon>();

        /// <summary>
        ///     CTOR
        /// </summary>
        public Combatant(Vector3 position)
            : base(position)
        {
        }

        /// <summary>
        ///     The currently selected weapon of the Combatant.
        /// </summary>
        public Weapon CurrentWeapon
        {
            get { return _CurrentWeapon; }
        }

        /// <summary>
        ///     This collection represents all the weapons a Combatant currently has.
        /// </summary>
        public IEnumerable<Weapon> Weapons
        {
            get { return _CurrentWeapons; }
        }

        /// <summary>
        ///     This value represents the health of the player. Starts at 100, player dies at 0.
        /// </summary>
        public virtual Double Health
        {
            get { return _Health; }
        }

        /// <summary>
        ///     This function gives a weapon to the Combatant IF they don't already have it.
        /// </summary>
        /// <param name="weapon"></param>
        public void GiveWeapon(Weapon weapon)
        {
            _CurrentWeapons.Add(weapon);
            _CurrentWeapon = weapon;
        }

        /// <summary>
        ///     This function gives ammo to the Combatant for a specific weapon. Does nothing if the Combatant doesnt have that
        ///     weapon.
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="ammoCount"></param>
        public void GiveAmmo(Weapon weapon, Int32 ammoCount)
        {
            if (ammoCount > 0 && _CurrentWeapons.SingleOrDefault(w => w == weapon) != null)
            {
                _CurrentWeapons.Single(w => w == weapon).IncreaseAmmo(ammoCount);
            }
        }

        /// <summary>
        ///     This method fires the currently selected weapon.
        /// </summary>
        public void FireCurrentWeapon()
        {
            _CurrentWeapon.Fire();
        }

        /// <summary>
        ///     This function damages the mortal.
        /// </summary>
        /// <param name="damage"></param>
        public virtual void Damage(Double damage)
        {
            if (damage > 0) _Health -= damage;
        }

        /// <summary>
        ///     This function heals the mortal.
        /// </summary>
        /// <param name="healAmount"></param>
        public virtual void Heal(Int32 healAmount)
        {
            if (healAmount > 0) _Health += healAmount;
        }

        public override void Update(Double msSinceLastUpdate)
        {
            _JumpCoolDown -= msSinceLastUpdate;
            if (!IsOnFloor())
            {
                Velocity.Z -= Level.GRAVITY*(msSinceLastUpdate/1000);
            }
            else
            {
                Velocity *= 0.9;
                if (Velocity.Z <= 0)
                {
                    Velocity.Z = 0;
                }
            }

            if (System.Math.Abs(Velocity.Z) < 0.01f)
            {
                if (Game.CurrentLevel.IsSolid(Position.X, Position.Y, Position.Z - COLLISION_HEIGHT + 1))
                {
                    Position.Z++;
                }
            }

            if (Velocity.Z > 0 && Game.CurrentLevel.IsSolid(Position.X, Position.Y, Position.Z + (COLLISION_HEIGHT*0.5)))
            {
                Velocity.Z = 0;

                Position.Z--;
            }

            float xRadius = Velocity.X > 0 ? COLLISION_RADIUS : -COLLISION_RADIUS;
            float yRadius = Velocity.Y > 0 ? COLLISION_RADIUS : -COLLISION_RADIUS;

            if (Game.CurrentLevel.IsSolid(Position.X + Velocity.X + xRadius, Position.Y, Position.Z - (COLLISION_HEIGHT - 2))) Velocity.X = 0;
            if (Game.CurrentLevel.IsSolid(Position.X + Velocity.X + xRadius, Position.Y, Position.Z)) Velocity.X = 0;

            if (Game.CurrentLevel.IsSolid(Position.X, Position.Y + Velocity.Y + yRadius, Position.Z - (COLLISION_HEIGHT - 2))) Velocity.Y = 0;
            if (Game.CurrentLevel.IsSolid(Position.X, Position.Y + Velocity.Y + yRadius, Position.Z)) Velocity.Y = 0;

            if (_Health <= 0 || Position.Z < -200) Kill();

            Position += Velocity*(msSinceLastUpdate/16); //the time factor stops fast machines from running too many updates
        }

        /// <summary>
        ///     This bool will tell the user if the combatant is on the floor or not.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsOnFloor()
        {
            return Game.CurrentLevel.IsSolid(Position.X, Position.Y, Position.Z - COLLISION_HEIGHT);
        }

        /// <summary>
        ///     This method will kill/set the combatant health to 0;
        /// </summary>
        public virtual void Kill()
        {
            new Explosion(Colour.Red, Position, 1);
            _Health = 0;
            Dispose();
        }

        public virtual void AttemptToHit(Projectile bullet)
        {
            if (bullet.Position.X + bullet.Size > Position.X - COLLISION_RADIUS && bullet.Position.X - bullet.Size < Position.X + COLLISION_RADIUS)
            {
                if (bullet.Position.Y + bullet.Size > Position.Y - COLLISION_RADIUS &&
                    bullet.Position.Y - bullet.Size < Position.Y + COLLISION_RADIUS)
                {
                    if (bullet.Position.Z + bullet.Size > Position.Z - COLLISION_HEIGHT &&
                        bullet.Position.Z - bullet.Size < Position.Z + COLLISION_HEIGHT)
                    {
                        Damage(bullet.Damage);
                        bullet.Dispose();
                    }
                }
            }
        }
    }
}