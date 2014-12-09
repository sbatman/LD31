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
using LD31.World;

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

        /// <summary>
        /// This function should be called first as it will initialize the renderer and other critical game objects.
        /// </summary>
        void Init()
        {
            InputHandler.Init();
            GraphicsManager.Init();
<<<<<<< HEAD

            Player = new Player(new Vector3(400, 400, 400));

=======
            Player = new Player(new Vector3(550, 450, 400));
>>>>>>> origin/development
            //give the player a default weapon and some ammo!
            Weapon defaultWeapon = new Weapon(new Colour(255, 255), Player);
            Player.GiveWeapon(defaultWeapon);
            Player.GiveAmmo(defaultWeapon, 10);

            CurrentLevel = new Level("GameLevel.txt");

            //create a default enemy!
           
        }

        /// <summary>
        /// This function handles drawing the game to screen
        /// </summary>
        void Draw()
        {
            GraphicsManager.StartDraw();

            //call draw for all game objects.

            foreach (GameObject o in GameObjects) o.Draw();

            CurrentLevel.Render();

            UI.Draw();

            GraphicsManager.EndDraw();
        }

        /// <summary>
        /// This function handles all logic updates
        /// </summary>
        void Update(Double msSinceLastUpdate)
        {
            if (Enemy == null || Enemy.Disposed) Enemy = new Enemy(Player.Position, Player);

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
                        ((Projectile)o).AttemptToHit(obj);
                    }
                }
            }

            //Clear out dead game objects
            GameObjects = GameObjects.Where(o => !o.Disposed).ToList();

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
            Stopwatch drawTime = new Stopwatch();

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
