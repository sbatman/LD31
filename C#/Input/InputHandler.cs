using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LD31.Input
{
    /// <summary>
    /// This class handles input checking for the game.
    /// </summary>
    static class InputHandler
    {
        /// <summary>
        /// This method checks to see if the key/button whatever that matches the relevant ButtonConcept has been pressed.
        /// </summary>
        /// <param name="buttonConcept"></param>
        /// <returns></returns>
        public static Boolean IsButtonDown(ButtonConcept buttonConcept)
        {
            switch(buttonConcept)
            {
                case ButtonConcept.Quit:
                    {
                        if (Keyboard.IsKeyDown(Key.Escape))
                        {
                            return true;
                        }
                    }
                    break;
            }

            return false;
        }
    }
}
