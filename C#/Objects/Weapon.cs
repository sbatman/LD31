﻿using System;
using LD31.Graphics;
using LD31.Math;

namespace LD31.Objects
{
    /// <summary>
    /// This class represents game weapons. The constructor is private as it is a class enum pattern implementation.
    /// All types of weapon are existing static instances.
    /// </summary>
    public class Weapon : GameObject
    {

        public enum WeaponTypes
        {
            PISTOL,
            SHOTGUN
        }
        /// <summary>
        /// The color of the projectiles for this weapon
        /// </summary>
        Colour _ProjectileColor;

        /// <summary>
        /// backing field
        /// </summary>
        Int32 _Ammunition;

        private Combatant _Owner;

        private Double _DamagePerShot = 30;

        private WeaponTypes _WeaponType;

        /// <summary>
        /// How much ammunition this weapon has.
        /// </summary>
        public Int32 Aummunition
        {
            get { return _Ammunition; }
        }

        public double DamagePerShot
        {
            get { return _DamagePerShot; }
            set { _DamagePerShot = value; }
        }

        public Combatant Owner
        {
            get { return _Owner; }
        }

        public WeaponTypes WeaponType
        {
            get { return _WeaponType; }
            set { _WeaponType = value; }
        }

        /// <summary>
        /// CTOR
        /// </summary>
        public Weapon(Colour projectileColour, Combatant owner)
        {
            _Owner = owner;
            _ProjectileColor = projectileColour;
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
            //   if (_Ammunition > 0)
            {
                _Ammunition -= 1;


                Camera camera = GraphicsManager.GetCamera();
                Vector3 position = new Vector3(camera.PositionX, camera.PositionY, camera.PositionZ - 5);

                Vector3 fireDirection = new Vector3(0)
                {
                    XY = Vector2.Rotate(new Vector2(0, -1), GraphicsManager.GetCamera().RotationZ)

                };

                fireDirection.Z =
                    Vector2.Rotate(new Vector2(0, 1), GraphicsManager.GetCamera().RotationX).X;


                //different guns have different projectiles when they fire!
                //if (this == Weapon.Pistol)
                //{
                Projectile bullet = new Projectile(this, position, fireDirection, _ProjectileColor);
                //}
                //if (this == Weapon.Shotgun)
                //{
                //    Projectile bullet1 = new Projectile(this, position, fireDirection, _ProjectileColor);
                //    Projectile bullet2 = new Projectile(this, position, fireDirection, _ProjectileColor);
                //    Projectile bullet3 = new Projectile(this, position, fireDirection, _ProjectileColor);
                //}
                //if (this == Weapon.DeathLaser)
                //{
                //    Projectile bullet1 = new Projectile(this, position, fireDirection, _ProjectileColor);
                //    Projectile bullet2 = new Projectile(this, position, fireDirection, _ProjectileColor);
                //    Projectile bullet3 = new Projectile(this, position, fireDirection, _ProjectileColor);
                //    Projectile bullet4 = new Projectile(this, position, fireDirection, _ProjectileColor);
                //    Projectile bullet5 = new Projectile(this, position, fireDirection, _ProjectileColor);
                //}

            }
            //   else
            {
                Console.WriteLine("Out of ammo for current weapon - no Bang :(");
            }


        }


    }
}
