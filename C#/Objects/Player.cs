using LD31.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Objects
{
    /// <summary>
    /// This class represents the player
    /// </summary>
    public class Player : GameObject
    {
        /// <summary>
        /// backing field
        /// </summary>
        Weapon currentWeapon = null;

        /// <summary>
        /// The currently selected weapon of the player.
        /// </summary>
        Weapon CurrentWeapon
        {
            get { return currentWeapon; }
        }

        /// <summary>
        /// backing field
        /// </summary>
        HashSet<Weapon> currentWeapons = new HashSet<Weapon>();

        /// <summary>
        /// This collection represents all the weapons a player currently has.
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
        /// backing field
        /// </summary>
        Int32 health = 100;

        /// <summary>
        /// This value represents the health of the player. Starts at 100, player dies at 0.
        /// </summary>
        Int32 Health
        {
            get
            {
                return health;
            }
        }

        /// <summary>
        /// This boolean states if the player is dead or not.
        /// </summary>
        Boolean IsDead
        {
            get
            {
                return health <= 0;
            }
        }

        /// <summary>
        /// Private backing field for position
        /// </summary>
        Vector2 position = new Vector2();

        /// <summary>
        /// This value represents the position of the player.
        /// </summary>
        Vector2 Position
        {
            get
            {
                return position;
            }
        }


        /// <summary>
        /// CTOR
        /// </summary>
        public Player(Vector2 position)
        {
            this.position = position;
        }


        /// <summary>
        /// This function moves the player.
        /// </summary>
        /// <param name="movement"></param>
        public void Move(Vector2 movement)
        {
            position.X += movement.X;
            position.Y += movement.Y;
        }


        /// <summary>
        /// This function damages the player.
        /// </summary>
        /// <param name="damage"></param>
        public void Damage(Int32 damage)
        {
            if (damage > 0) health -= damage;
        }


        /// <summary>
        /// This function heals the player.
        /// </summary>
        /// <param name="healAmount"></param>
        public void Heal(Int32 healAmount)
        {
            if (healAmount > 0) health += healAmount;
        }


        /// <summary>
        /// This function gives a weapon to the player IF they don't already have it.
        /// </summary>
        /// <param name="weaponType"></param>
        public void GiveWeapon(Weapon weapon)
        {
            currentWeapons.Add(weapon);
        }


        /// <summary>
        /// This function gives ammo to the player for a specific weapon. Does nothing if the player doesnt have that weapon.
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
