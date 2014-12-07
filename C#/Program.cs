using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31
{
    class Program
    {

        /// <summary>
        /// The main entry point for the game. Make sure to release ALL resource before it exits.
        /// </summary>
        /// <param name="args"></param>
        [STAThread] //we must declare the correct apartment state to use keyboard input. Clean this up and remove later!
        private static void Main(string[] args)
        {
            using (Game game = new Game())
            {
                game.Run();
            }
        }
    }
}
