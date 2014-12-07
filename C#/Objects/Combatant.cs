using System;
using System.Collections.Generic;
using System.Linq;
using LD31.Graphics;
using LD31.Math;

namespace LD31.Objects
{
    public abstract class Combatant : Moveable
    {
        /// <summary>
        /// backing field
        /// </summary>
        protected Weapon _CurrentWeapon = null;

        /// <summary>
        /// backing field
        /// </summary>
        protected readonly HashSet<Weapon> _CurrentWeapons = new HashSet<Weapon>();

        protected Int32 _CollisionHeight = 30;
        protected Int32 _CollisionRadius = 8;

        

        /// <summary>
        /// The currently selected weapon of the Combatant.
        /// </summary>
        public Weapon CurrentWeapon
        {
            get { return _CurrentWeapon; }
        }

        /// <summary>
        /// This collection represents all the weapons a Combatant currently has.
        /// </summary>
        public IEnumerable<Weapon> Weapons
        {
            get { return _CurrentWeapons; }
        }

        /// <summary>
        /// This value represents the health of the player. Starts at 100, player dies at 0.
        /// </summary>
        public virtual Int32 Health
        {
            get { return _Health; }
        }

        /// <summary>
        /// This boolean states if the player is dead or not.
        /// </summary>
        public override Boolean Alive
        {
            get { return _Health > 0; }
        }

        /// <summary>
        /// CTOR
        /// </summary>
        public Combatant(Vector3 position)
            : base(position)
        {
        }

        /// <summary>
        /// This function gives a weapon to the Combatant IF they don't already have it.
        /// </summary>
        /// <param name="weapon"></param>
        public void GiveWeapon(Weapon weapon)
        {
            _CurrentWeapons.Add(weapon);
            _CurrentWeapon = weapon;
        }


        /// <summary>
        /// This function gives ammo to the Combatant for a specific weapon. Does nothing if the Combatant doesnt have that weapon.
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
        /// This method fires the currently selected weapon.
        /// </summary>
        public void FireCurrentWeapon()
        {
            _CurrentWeapon.Fire();
        }

        /// <summary>
        /// backing field
        /// </summary>
        private Int32 _Health = 100;

        /// <summary>
        /// This function damages the mortal.
        /// </summary>
        /// <param name="damage"></param>
        public virtual void Damage(Int32 damage)
        {
            if (damage > 0) _Health -= damage;
        }

        /// <summary>
        /// This function heals the mortal.
        /// </summary>
        /// <param name="healAmount"></param>
        public virtual void Heal(Int32 healAmount)
        {
            if (healAmount > 0) _Health += healAmount;
        }

        public override void Update(Double msSinceLastUpdate)
        {
            if (!IsOnFloor())
            {
                Velocity.Z -= Level.GRAVITY * (msSinceLastUpdate / 1000);
            }
            else
            {
                Velocity *= 0.9;
                if (Velocity.Z <= 0)
                {
                    Velocity.Z = 0;
                }
            }

            if (Velocity.Z>0 && Game.CurrentLevel.IsSolid(Position.X, Position.Y, Position.Z+ 10))
            {
                Velocity.Z = 0;
            }

            float xRadius = Velocity.X > 0 ? _CollisionRadius : -_CollisionRadius;
            float yRadius = Velocity.Y > 0 ? _CollisionRadius : -_CollisionRadius;

            if (Game.CurrentLevel.IsSolid(Position.X + Velocity.X + xRadius, Position.Y, Position.Z))
            {
                Velocity.X = 0;
            }

            if (Game.CurrentLevel.IsSolid(Position.X, Position.Y + Velocity.Y + yRadius, Position.Z))
            {
                Velocity.Y = 0;
            }

            Position += Velocity;
        }

        public virtual bool IsOnFloor()
        {
            return Game.CurrentLevel.IsSolid(Position.X, Position.Y, Position.Z - _CollisionHeight);
        }
    }
}
