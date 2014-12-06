using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Objects
{
    public class Weapon : GameObject
    {
        public static readonly Weapon Pistol = new Weapon(0);


        /// <summary>
        /// backing field
        /// </summary>
        Int32 ammunition;

        /// <summary>
        /// How much ammunition this weapon has.
        /// </summary>
        public Int32 Aummunition
        {
            get { return ammunition; }
        }


        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ammunition"></param>
        private Weapon(Int32 ammunition)
        {
            this.ammunition = ammunition;
        }


        /// <summary>
        /// This method gives ammo to the weapon
        /// </summary>
        /// <param name="ammoToGive"></param>
        public void IncreaseAmmo(Int32 ammoToGive)
        {
            if (ammoToGive > 0) ammunition += ammoToGive;
        }


        /// <summary>
        /// This method takes ammo from the weapon.
        /// </summary>
        /// <param name="ammoToTake"></param>
        public void DecreaseAmmo(Int32 ammoToTake)
        {
            if (ammoToTake > 0) ammunition -= ammoToTake;
        }


        /// <summary>
        /// This method fires the weapon if it has ammo.
        /// </summary>
        public void Fire()
        {
            if(ammunition > 0) ammunition -= 1;
        }
    }
}
