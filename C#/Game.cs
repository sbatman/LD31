using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using LD31.Graphics;
using LD31.Input;
using LD31.Math;
using LD31.Objects;
using LD31.World;

namespace LD31
{
    internal class Game : IDisposable
    {
        /// <summary>
        ///     This flag shows if the game is still running or not.
        /// </summary>
        private static Boolean _GameRunning = true;

        /// <summary>
        ///     The player object.
        /// </summary>
        public static Player Player;

        public static List<Enemy> Enemys = new List<Enemy>();

        /// <summary>
        ///     The current level
        /// </summary>
        public static Level CurrentLevel;

        /// <summary>
        ///     A collection of all gameobjects currently available
        /// </summary>
        public static List<GameObject> GameObjects = new List<GameObject>();

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
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

        /// <summary>
        ///     This function should be called first as it will initialize the renderer and other critical game objects.
        /// </summary>
        private void Init()
        {
            InputHandler.Init();
            GraphicsManager.Init();

            Player = new Player(new Vector3(550, 450, 400));

            //give the player a default weapon and some ammo!
            Weapon defaultWeapon = new Weapon(new Colour(255, 255), Player);
            Player.GiveWeapon(defaultWeapon);
            Player.GiveAmmo(defaultWeapon, 10);

            CurrentLevel = new Level("GameLevel.txt");
        }

        /// <summary>
        /// This function handles drawing the game to screen
        /// </summary>
        private void Draw()
        {
            GraphicsManager.StartDraw();

            //call draw for all game objects.

            foreach (GameObject o in GameObjects) o.Draw();

            CurrentLevel.Render();

            UI.Draw();


            String healthString = String.Format("Health: {0}", Player.Health);
            String ammoString = String.Format("Ammo: {0}", Player.CurrentWeapon.Aummunition);
            UI.DrawTextToScreen(ammoString, 0, 30);
            UI.DrawTextToScreen(healthString, 0, 20);
            
            GraphicsManager.EndDraw();
        }

        /// <summary>
        ///     This function handles all logic updates
        /// </summary>
        private void Update(Double msSinceLastUpdate)
        {
            Enemys = Enemys.Where(a => !a.Disposed).ToList();
            while (Enemys.Count < 5) Enemys.Add(new Enemy(Player.Position, Player));

            InputHandler.Update();
            GraphicsManager.Update();
            //call update for all game objects.
            List<GameObject> currentObjects = new List<GameObject>(GameObjects);
            foreach (GameObject o in currentObjects)
            {
                o.Update(msSinceLastUpdate);
                if (o is Projectile)
                {
                    foreach (Combatant obj in currentObjects.OfType<Combatant>())
                    {
                        ((Projectile) o).AttemptToHit(obj);
                    }
                }
            }

            //Clear out dead game objects
            GameObjects = GameObjects.Where(o => !o.Disposed).ToList();

            //Allow user to quit the game.
            if (InputHandler.IsButtonDown(ButtonConcept.QUIT))
            {
                _GameRunning = false;
            }
        }

        /// <summary>
        ///     The main game run function.
        /// </summary>
        public void Run()
        {
            Init();
            Stopwatch updateTime = new Stopwatch();
            Double lastUpdateMS = 0;

            updateTime.Start();
            while (_GameRunning)
            {
                Draw();
                double currentUpdateMS = updateTime.ElapsedMilliseconds;
                Update(currentUpdateMS - lastUpdateMS);
                lastUpdateMS = currentUpdateMS;
                Thread.Sleep(0);
            }

            //exit logic
        }
    }
}
