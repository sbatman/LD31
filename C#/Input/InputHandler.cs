using System;
using System.Runtime.InteropServices;

namespace LD31.Input
{
    /// <summary>
    /// This class handles input checking for the game.
    /// </summary>
    static class InputHandler
    {
        /// <summary>
        /// Native interop methods. Should not be accessed outside of InputHandler!
        /// </summary>
        static class NativeMethods
        {
            public delegate void KeyboardCallBack(Int32 key);
            public delegate void MouseMoveCallBack(Int32 x, Int32 y);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerSetKeyboardDownCallback(KeyboardCallBack callback);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerSetKeyboardUpCallback(KeyboardCallBack callback);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerSetMouseMoveCallback(MouseMoveCallBack callback);

        }

        static NativeMethods.KeyboardCallBack KeyboardDownCallBack;
        static NativeMethods.KeyboardCallBack KeyboardUpCallBack;
        static NativeMethods.MouseMoveCallBack MouseMoveCallBack;

        /// <summary>
        /// This array of bools holds all the current key states
        /// </summary>
        private static Boolean[] _KeyStates = new Boolean[255];

        /// <summary>
        /// This array of bools holds all the previous keystates
        /// </summary>
        private static Boolean[] _PastKeyStates = new Boolean[255];

        /// <summary>
        /// Input handle initialization. Must be called before the input handler is used!
        /// </summary>
        public static void Init()
        {
            KeyboardDownCallBack = HandleKeyDown;
            KeyboardUpCallBack = HandleKeyUp;
            MouseMoveCallBack = HandleMouseMove;
            GC.KeepAlive(KeyboardDownCallBack);
            GC.KeepAlive(KeyboardUpCallBack);
            GC.KeepAlive(MouseMoveCallBack);
            NativeMethods.GraphicsManagerSetKeyboardDownCallback(KeyboardDownCallBack);
            NativeMethods.GraphicsManagerSetKeyboardUpCallback(KeyboardUpCallBack);
            NativeMethods.GraphicsManagerSetMouseMoveCallback(MouseMoveCallBack);
        }


        /// <summary>
        /// This method is the callback used to handle mouse movement.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private static void HandleMouseMove(Int32 x, Int32 y)
        {
            Graphics.GraphicsManager.GetCamera().Rotatate(x * 0.2f, y * 0.2f);
            Console.WriteLine("Mouse Moved Callback {0}x{1}", x, y);
        }

        /// <summary>
        /// This callback handles when a key is pressed down
        /// </summary>
        /// <param name="key"></param>
        private static void HandleKeyDown(Int32 key)
        {
            _KeyStates[key] = true;
        }

        /// <summary>
        /// This callback handles when a key is release.
        /// </summary>
        /// <param name="key"></param>
        private static void HandleKeyUp(Int32 key)
        {
            _KeyStates[key] = false;
        }

        /// <summary>
        /// This method updates the keystate collections.
        /// </summary>
        public static void Update()
        {
            Array.Copy(_KeyStates, _PastKeyStates,255);
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
                case ButtonConcept.Fire: return (_KeyStates[0x11]); break;
                case ButtonConcept.TestButton1: return (_KeyStates[0x24]); break;
            }

            return false;
        }

        public static Boolean WasButtonPressed(ButtonConcept buttonConcept)
        {
            switch (buttonConcept)
            {
                case ButtonConcept.Fire: return (_KeyStates[0x11] && !_PastKeyStates[0x11]); break;
                case ButtonConcept.TestButton1: return (_KeyStates[0x24] && !_PastKeyStates[0x24]); break;
            }

            return false;

        }

        public static Boolean WasButtonReleased(ButtonConcept buttonConcept)
        {
            switch (buttonConcept)
            {
                case ButtonConcept.Fire: return (!_KeyStates[0x11] && _PastKeyStates[0x11]); break;
                case ButtonConcept.TestButton1: return (!_KeyStates[0x24] && _PastKeyStates[0x24]); break;
            }

            return false;

        }
    }
}
