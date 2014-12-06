using LD31.Graphics;
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
        /// This function should be called first as it will initialize the renderer and other critical game objects.
        /// </summary>
        static void Init()
        {
            GraphicsManager.Init();
        }

        /// <summary>
        /// This function handles drawing the game to screen
        /// </summary>
        static void Draw()
        {
            GraphicsManager.StartDraw();

            //MAIN DRAW LOGIC HERE

            GraphicsManager.EndDraw();
        }

        /// <summary>
        /// This function handles all logic updates
        /// </summary>
        static void Update()
        {
            GraphicsManager.Update();
            //update logic here.
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
        static void Main(string[] args)
        {
            Init();

            while(GameRunning)
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
