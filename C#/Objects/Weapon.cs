using LD31.Graphics;
using LD31.Math;
using System;

namespace LD31.Objects
{
    /// <summary>
    /// This class represents game weapons. The constructor is private as it is a class enum pattern implementation.
    /// All types of weapon are existing static instances.
    /// </summary>
    public class Weapon : GameObject
    {
        /// <summary>
        /// This instance represents a pistol
        /// </summary>
        public static readonly Weapon Pistol = new Weapon(0);

        /// <summary>
        /// backing field
        /// </summary>
        Int32 _Ammunition;

        /// <summary>
        /// How much ammunition this weapon has.
        /// </summary>
        public Int32 Aummunition
        {
            get { return _Ammunition; }
        }


        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="ammunition"></param>
        private Weapon(Int32 ammunition)
        {
            _Ammunition = ammunition;
        }


        /// <summary>
        /// This method gives ammo to the weapon
        /// </summary>
        /// <param name="ammoToGive"></param>
        public void IncreaseAmmo(Int32 ammoToGive)
        {
            if (ammoToGive > 0) _Ammunition += ammoToGive;
        }


        /// <summary>
        /// This method takes ammo from the weapon.
        /// </summary>
        /// <param name="ammoToTake"></param>
        public void DecreaseAmmo(Int32 ammoToTake)
        {
            if (ammoToTake > 0) _Ammunition -= ammoToTake;
        }


        /// <summary>
        /// This method fires the weapon if it has ammo.
        /// </summary>
        public void Fire()
        {
            if (_Ammunition > 0)
            {
                _Ammunition -= 1;

                Camera camera = GraphicsManager.GetCamera();
                Vector3 position = new Vector3(camera.PositionX, camera.PositionY, camera.PositionZ);
                Projectile bullet = new Projectile(position, camera.RotationZ);
            }
            else
            {
                Console.WriteLine("Out of ammo for current weapon - no Bang :(");
            }


        }

        public override void Update(Double msSinceLastUpdate)
        {
            
        }
    }
}
