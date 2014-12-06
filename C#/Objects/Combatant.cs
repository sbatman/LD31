using System;
using System.Collections.Generic;
using System.Linq;
using LD31.Math;

namespace LD31.Objects
{
    public abstract class Combatant : Moveable
    {
        /// <summary>
        /// backing field
        /// </summary>
        protected readonly Weapon _CurrentWeapon = null;

        /// <summary>
        /// The currently selected weapon of the Combatant.
        /// </summary>
        public Weapon CurrentWeapon
        {
            get { return _CurrentWeapon; }
        }

        /// <summary>
        /// backing field
        /// </summary>
        protected readonly HashSet<Weapon> _CurrentWeapons = new HashSet<Weapon>();

        /// <summary>
        /// This collection represents all the weapons a Combatant currently has.
        /// </summary>
        public IEnumerable<Weapon> CurrentWeapons
        {
            get
            {
                return _CurrentWeapons;
            }
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
        /// <param name="weaponType"></param>
        public void GiveWeapon(Weapon weapon)
        {
            _CurrentWeapons.Add(weapon);
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
    }
}
