using System;
using System.Runtime.InteropServices;

namespace LD31.Input
{
    /// <summary>
    /// This class handles input checking for the game.
    /// </summary>
    static class InputHandler
    {
        static class NativeMethods
        {
            public delegate void KeyboardCallBack(Int32 key);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerSetKeyboardDownCallback(KeyboardCallBack callback);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerSetKeyboardUpCallback(KeyboardCallBack callback);

        }

        private static bool[] _KeyStates = new bool[255];
        private static bool[] _PastKeyStates = new bool[255];

        public static void Init()
        {
            NativeMethods.GraphicsManagerSetKeyboardDownCallback(HandelKeyDown);
            NativeMethods.GraphicsManagerSetKeyboardUpCallback(HandelKeyUp);
        }

        private static void HandelKeyDown(Int32 key)
        {
            _KeyStates[key] = true;
        }

        private static void HandelKeyUp(Int32 key)
        {
            _KeyStates[key] = false;
        }

        public static void Update()
        {
            _PastKeyStates = _KeyStates;
        }

        /// <summary>
        /// This method checks to see if the key/button whatever that matches the relevant ButtonConcept has been pressed.
        /// </summary>
        /// <param name="buttonConcept"></param>
        /// <returns></returns>
        public static Boolean IsButtonDown(ButtonConcept buttonConcept)
        {
            switch (buttonConcept)
            {
                case ButtonConcept.Quit: return (_KeyStates[0x1B]); break;
                case ButtonConcept.Jump: return (_KeyStates[0x20]); break;
                case ButtonConcept.Forward: return (_KeyStates[0x26] || _KeyStates[0x57]); break;
                case ButtonConcept.Backward: return (_KeyStates[0x28] || _KeyStates[0x53]); break;
                case ButtonConcept.Left: return (_KeyStates[0x25] || _KeyStates[0x41]); break;
                case ButtonConcept.Right: return (_KeyStates[0x27] || _KeyStates[0x44]); break;
            }

            return false;
        }
    }
}
