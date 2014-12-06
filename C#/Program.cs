﻿using LD31.Graphics;
using LD31.Input;
using LD31.Math;
using LD31.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LD31
{
    class Program
    {
        /// <summary>
        /// This value is used to stop the main update loop running too fast.
        /// </summary>
        private const Int32 UpdateDelay = 16;

        /// <summary>
        /// This flag shows if the game is still running or not. 
        /// </summary>
        private static Boolean GameRunning = true;

        /// <summary>
        /// The player object.
        /// </summary>
        private static Player player = new Player(new Vector2());

        /// <summary>
        /// This function should be called first as it will initialize the renderer and other critical game objects.
        /// </summary>
        static void Init()
        {
            //give the player a default weapon!
            player.GiveWeapon(Weapon.Pistol);

            GraphicsManager.Init();
        }

        /// <summary>
        /// This function handles drawing the game to screen
        /// </summary>
        static void Draw()
        {
            GraphicsManager.StartDraw();

            for (int x = -16; x < 16; x++)
            {
                for (int y = -16; y < 16; y++)
                {
                    for (int z = -8; z < 8; z++)
                    {
                        GraphicsManager.DrawWorldVoxel(x, y, z, 255, 255, 255, 10);
                    }
                }
            }

            //GraphicsManager.DrawWorldVoxel(-1, -1, -1, 255, 255, 255, 255);
            //GraphicsManager.DrawWorldVoxel(0, 0, 0, 255, 255, 255, 255);
            //GraphicsManager.DrawWorldVoxel(1, 1, 1, 255, 255, 255, 255);

            GraphicsManager.EndDraw();
        }

        /// <summary>
        /// This function handles all logic updates
        /// </summary>
        static void Update()
        {
            GraphicsManager.Update();
            //update logic here.

            //Allow user to quit the game.
            if (InputHandler.IsButtonDown(ButtonConcept.Quit))
            {
                GameRunning = false;
            }
        }

        /// <summary>
        /// This function should be called last as it will release memory and cleanup objects.
        /// </summary>
        static void Destroy()
        {
            GraphicsManager.Destroy();
        }

        /// <summary>
        /// The main entry point for the game. Make sure to release ALL resource before it exits.
        /// </summary>
        /// <param name="args"></param>
        [STAThread] //we must declare the correct apartment state to use keyboard input. Clean this up and remove later!
        static void Main(string[] args)
        {
            Init();

            while (GameRunning)
            {
                Draw();
                Update();

                Thread.Sleep(UpdateDelay);
            }

            //exit logic


            //cleanup logic
            Destroy();
        }
    }
}
