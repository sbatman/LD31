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

            GraphicsManager.EndDraw();
        }

        /// <summary>
        /// This function handles all logic updates
        /// </summary>
        static void Update()
        {
            GraphicsManager.Destroy();
        }

        /// <summary>
        /// This function should be called last as it will release memory and cleanup objects.
        /// </summary>
        static void Destroy();

        /// <summary>
        /// The main entry point for the game. Make sure to release ALL resource before it exits.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Init();

            while(true)//FIX THIS
            {
                Draw();
                Update();

                Thread.Sleep(16);
            }

            Destroy();
        }
    }
}
