﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public static Player _Player;

        public static Level _CurrentLevel;

        public static List<GameObject> _GameObjects = new List<GameObject>();

        /// <summary>
        /// This function should be called first as it will initialize the renderer and other critical game objects.
        /// </summary>
        void Init()
        {
            GraphicsManager.Init();
            InputHandler.Init();

            _Player = new Player(new Vector3(200, 200, 200));

            //give the player a default weapon!
            _Player.GiveWeapon(Weapon.Pistol);
            _CurrentLevel = new Level(30, 30,10);

            for (int x = 0; x < 30; x++)
            {
                for (int y = 0; y < 30; y++)
                {
                    for (int z = 0; z < 5; z++)
                    {
                        if (((x == 0 || x == 29) || (y == 0 || y == 29)) || z == 0) _CurrentLevel.SetBlock(new Block(), x, y, z);
                    }
                }
            }
            _CurrentLevel.SetBlock(new Block(), 5, 5, 1);
            _CurrentLevel.SetBlock(new Block(), 5, 6, 2);
            _CurrentLevel.SetBlock(new Block(), 5, 7, 3);
            _CurrentLevel.SetBlock(new Block(), 5, 8, 4);
            _CurrentLevel.SetBlock(new Block(), 5, 9, 5);
        }

        /// <summary>
        /// This function handles drawing the game to screen
        /// </summary>
        void Draw()
        {
            GraphicsManager.StartDraw();

            _CurrentLevel.Render();

            GraphicsManager.EndDraw();
        }

        /// <summary>
        /// This function handles all logic updates
        /// </summary>
        void Update(Double msSinceLastUpdate)
        {
            GraphicsManager.Update();
            InputHandler.Update();

            foreach (GameObject o in _GameObjects) o.Update(msSinceLastUpdate);

            //Allow user to quit the game.
            if (InputHandler.IsButtonDown(ButtonConcept.Quit))
            {
                _GameRunning = false;
            }
        }

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
            foreach (GameObject o in new List<GameObject>(_GameObjects)) o.Dispose();
            _GameObjects.Clear();
            _GameObjects = null;
            _Player = null;
            _CurrentLevel.Dispose();
            GraphicsManager.Destroy();
        }
    }
}
