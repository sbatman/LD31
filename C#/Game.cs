using System;
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

        public static Enemy Enemy;

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

            var playerPosition = new Vector3(400, 400, 400);
            Player = new Player(playerPosition);
            //give the player a default weapon and some ammo!
            Weapon defaultWeapon = Weapon.Shotgun;
            Player.GiveWeapon(defaultWeapon);
            Player.GiveAmmo(defaultWeapon, 10);

            CurrentLevel = new Level(30, 30,10);

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

            //create a default enemy!
            Enemy = new Enemy(new Vector3(30, 30, 0), Player);
        }

        /// <summary>
        /// This function handles drawing the game to screen
        /// </summary>
        void Draw()
        {
            GraphicsManager.StartDraw();

            //call draw for all game objects.
            if (LastExplosion != null) LastExplosion.Draw();
            foreach (GameObject o in GameObjects) o.Draw();

            CurrentLevel.Render();

            GraphicsManager.EndDraw();
        }

        /// <summary>
        /// This function handles all logic updates
        /// </summary>
        void Update(Double msSinceLastUpdate)
        {
            InputHandler.Update();
            GraphicsManager.Update();
            //call update for all game objects.
            foreach (GameObject o in new List<GameObject>(GameObjects)) o.Update(msSinceLastUpdate);

            //Clear out dead game objects
            GameObjects = GameObjects.Where(o => o.Alive).ToList();


            if (InputHandler.WasButtonPressed(ButtonConcept.TestButton1) && Enemy.Alive)
            {
                Enemy.Kill();
                LastExplosion = new Explosion(Colour.Red, Enemy.Position, 1);
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
