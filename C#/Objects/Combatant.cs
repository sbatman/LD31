using LD31.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Objects
{
    public abstract class Combatant : Mortal
    {
        /// <summary>
        /// backing field
        /// </summary>
        protected Weapon currentWeapon = null;

        /// <summary>
        /// The currently selected weapon of the Combatant.
        /// </summary>
        public Weapon CurrentWeapon
        {
            get { return currentWeapon; }
        }

        /// <summary>
        /// backing field
        /// </summary>
        protected HashSet<Weapon> currentWeapons = new HashSet<Weapon>();

        /// <summary>
        /// This collection represents all the weapons a Combatant currently has.
        /// </summary>
        public IEnumerable<Weapon> CurrentWeapons
        {
            get
            {
                foreach (Weapon weaponType in currentWeapons)
                {
                    yield return weaponType;
                }
            }
        }


        /// <summary>
        /// CTOR
        /// </summary>
        public Combatant(Vector2 position)
            : base(position)
        {
        }

        /// <summary>
        /// This function gives a weapon to the Combatant IF they don't already have it.
        /// </summary>
        /// <param name="weaponType"></param>
        public void GiveWeapon(Weapon weapon)
        {
            currentWeapons.Add(weapon);
        }


        /// <summary>
        /// This function gives ammo to the Combatant for a specific weapon. Does nothing if the Combatant doesnt have that weapon.
        /// </summary>
        /// <param name="weaponType"></param>
        /// <param name="ammoCount"></param>
        public void GiveAmmo(Weapon weapon, Int32 ammoCount)
        {
            if (ammoCount > 0 && currentWeapons.Where(w => w == weapon).SingleOrDefault() != null) 
            {
                currentWeapons.Where(w => w == weapon).Single().IncreaseAmmo(ammoCount);
            }
        }


        /// <summary>
        /// This method fires the currently selected weapon.
        /// </summary>
        public void FireCurrentWeapon()
        {
            currentWeapon.Fire();
        }
    }
}
