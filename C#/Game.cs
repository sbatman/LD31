﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Threading;
using LD31.Graphics;
using LD31.Input;
using LD31.Math;
using LD31.Objects;

namespace LD31
{
    class Game : IDisposable
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
        public static Player Player;

        /// <summary>
        /// The current level
        /// </summary>
        public static Level CurrentLevel;

        /// <summary>
        /// A collection of all gameobjects currently available
        /// </summary>
        public static List<GameObject> GameObjects = new List<GameObject>();

        public static Graphics.Explosion LastExplosion;

        /// <summary>
        /// This function should be called first as it will initialize the renderer and other critical game objects.
        /// </summary>
        void Init()
        {
            InputHandler.Init();
            GraphicsManager.Init();


            Player = new Player(new Vector3(200, 200, 200));

            //give the player a default weapon!
            Player.GiveWeapon(Weapon.Pistol);
            CurrentLevel = new Level(30, 30, 10);

            for (int x = 0; x < 30; x++)
            {
                for (int y = 0; y < 30; y++)
                {
                    for (int z = 0; z < 5; z++)
                    {
                        if (((x == 0 || x == 29) || (y == 0 || y == 29)) || z == 0) CurrentLevel.SetBlock(new Block(), x, y, z);
                    }
                }
            }
            CurrentLevel.SetBlock(new Block(), 5, 5, 1);
            CurrentLevel.SetBlock(new Block(), 5, 6, 2);
            CurrentLevel.SetBlock(new Block(), 5, 7, 3);
            CurrentLevel.SetBlock(new Block(), 5, 8, 4);
            CurrentLevel.SetBlock(new Block(), 5, 9, 5);
        }

        /// <summary>
        /// This function handles drawing the game to screen
        /// </summary>
        void Draw()
        {
            GraphicsManager.StartDraw();

            CurrentLevel.Render();
            if (LastExplosion != null) LastExplosion.Draw();

            GraphicsManager.EndDraw();
        }

        /// <summary>
        /// This function handles all logic updates
        /// </summary>
        void Update(Double msSinceLastUpdate)
        {
            InputHandler.Update();
            GraphicsManager.Update();


            foreach (GameObject o in GameObjects) o.Update(msSinceLastUpdate);

            if (InputHandler.WasButtonPressed(ButtonConcept.TestButton1))
            {
                LastExplosion = new Explosion(new Colour(255, 0, 0, 255), new Vector3(160, 200, 25), 1);
            }

            if (InputHandler.WasButtonPressed(ButtonConcept.Fire))
            {
                Console.WriteLine("Bang");
                Camera camera = GraphicsManager.GetCamera();
                Vector3 position = new Vector3(camera.PositionX, camera.PositionY, camera.PositionZ);
                Projectile bullet = new Projectile(position);
            }

            //Allow user to quit the game.
            if (InputHandler.IsButtonDown(ButtonConcept.Quit))
            {
                _GameRunning = false;
            }
        }

        /// <summary>
        /// The main game run function.
        /// </summary>
        public void Run()
        {
            Init();
            Stopwatch updateTime = new Stopwatch();
            updateTime.Start();
            while (_GameRunning)
            {
                Draw();
                Update(updateTime.ElapsedMilliseconds);
                updateTime.Restart();

                Thread.Sleep(UPDATE_DELAY);
            }

            //exit logic
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            foreach (GameObject o in new List<GameObject>(GameObjects)) o.Dispose();
            GameObjects.Clear();
            GameObjects = null;
            Player = null;
            CurrentLevel.Dispose();
            GraphicsManager.Destroy();
        }
    }
}
