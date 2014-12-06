using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LD31.Graphics;
using LD31.Input;
using LD31.Math;
using LD31.Objects;

namespace LD31
{
    class Program
    {
        /// <summary>
        /// This value is used to stop the main update loop running too fast.
        /// </summary>
        private const Int32 UPDATE_DELAY = 8;

        /// <summary>
        /// This flag shows if the game is still running or not. 
        /// </summary>
        private static Boolean _GameRunning = true;

        /// <summary>
        /// The player object.
        /// </summary>
        private static Player _Player;

        private static Level _CurrentLevel;

        /// <summary>
        /// This function should be called first as it will initialize the renderer and other critical game objects.
        /// </summary>
        static void Init()
        {


            GraphicsManager.Init();

            _Player = new Player(new Vector3(200, 200, 200));
           

            //give the player a default weapon!
            _Player.GiveWeapon(Weapon.Pistol);
            _CurrentLevel = new Level(30, 30, 5);

            for (int x = 0; x < 30; x++)
            {
                for (int z = 0; z < 30; z++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        if(((x==0||x==29)||(z==0||z==29))|| y==0)_CurrentLevel.SetBlock(new Block(), x, y, z);
                    }
                }
            }
        }

        /// <summary>
        /// This function handles drawing the game to screen
        /// </summary>
        static void Draw()
        {
            GraphicsManager.StartDraw();

            _CurrentLevel.Render();

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
                _GameRunning = false;
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

            while (_GameRunning)
            {
                Draw();
                Update();

                Thread.Sleep(UPDATE_DELAY);
            }

            //exit logic


            //cleanup logic
            Destroy();
        }
    }
}
