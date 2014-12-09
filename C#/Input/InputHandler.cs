using System;
using System.Runtime.InteropServices;
using LD31.Graphics;

namespace LD31.Input
{
    /// <summary>
    ///     This class handles input checking for the game.
    /// </summary>
    internal static class InputHandler
    {
        private static NativeMethods.KeyboardCallBack _KeyboardDownCallBack;
        private static NativeMethods.KeyboardCallBack _KeyboardUpCallBack;
        private static NativeMethods.MouseMoveCallBack _MouseMoveCallBack;
        private static NativeMethods.MouseActionCallBack _MousePressCallBack;
        private static NativeMethods.MouseActionCallBack _MouseReleaseCallBack;

        /// <summary>
        ///     This array of bools holds all the current key states
        /// </summary>
        private static readonly Boolean[] _KeyStates = new Boolean[255];

        /// <summary>
        ///     This array of bools holds all the previous keystates
        /// </summary>
        private static readonly Boolean[] _PastKeyStates = new Boolean[255];

        /// <summary>
        ///     This array of bools holds all the current key MouseButtonstates
        /// </summary>
        private static readonly Boolean[] _MouseButtonStates = new Boolean[3];

        /// <summary>
        ///     This array of bools holds all the previous MouseButtonstates
        /// </summary>
        private static readonly Boolean[] _PastMouseButtonStates = new Boolean[3];

        /// <summary>
        ///     Input handle initialization. Must be called before the input handler is used!
        /// </summary>
        public static void Init()
        {
            _KeyboardDownCallBack = HandleKeyDown;
            _KeyboardUpCallBack = HandleKeyUp;
            _MouseMoveCallBack = HandleMouseMove;
            _MousePressCallBack = HandleMousePress;
            _MouseReleaseCallBack = HandleMouseRelase;
            GC.KeepAlive(_KeyboardDownCallBack);
            GC.KeepAlive(_KeyboardUpCallBack);
            GC.KeepAlive(_MouseMoveCallBack);
            GC.KeepAlive(_MousePressCallBack);
            GC.KeepAlive(_MouseReleaseCallBack);
            NativeMethods.GraphicsManagerSetKeyboardDownCallback(_KeyboardDownCallBack);
            NativeMethods.GraphicsManagerSetKeyboardUpCallback(_KeyboardUpCallBack);
            NativeMethods.GraphicsManagerSetMouseMoveCallback(_MouseMoveCallBack);
            NativeMethods.GraphicsManagerSetMousePressCallback(_MousePressCallBack);
            NativeMethods.GraphicsManagerSetMouseReleaseCallback(_MouseReleaseCallBack);
        }

        /// <summary>
        ///     This method is the callback used to handle mouse movement.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private static void HandleMouseMove(Int32 x, Int32 y)
        {
            GraphicsManager.GetCamera().Rotatate(x*0.2f, y*0.2f);
        }

        private static void HandleMousePress(Int32 button)
        {
            if (button < 0 || button > 2) return;
            _MouseButtonStates[button] = true;
        }

        private static void HandleMouseRelase(Int32 button)
        {
            if (button < 0 || button > 2) return;
            _MouseButtonStates[button] = false;
        }

        /// <summary>
        ///     This callback handles when a key is pressed down
        /// </summary>
        /// <param name="key"></param>
        private static void HandleKeyDown(Int32 key)
        {
            _KeyStates[key] = true;
        }

        /// <summary>
        ///     This callback handles when a key is release.
        /// </summary>
        /// <param name="key"></param>
        private static void HandleKeyUp(Int32 key)
        {
            _KeyStates[key] = false;
        }

        /// <summary>
        ///     This method updates the keystate collections.
        /// </summary>
        public static void Update()
        {
            Array.Copy(_KeyStates, _PastKeyStates, 255);
            Array.Copy(_MouseButtonStates, _PastMouseButtonStates, 3);
        }

        /// <summary>
        ///     This method checks to see if the key/button whatever that matches the relevant ButtonConcept has been pressed.
        /// </summary>
        /// <param name="buttonConcept"></param>
        /// <returns></returns>
        public static Boolean IsButtonDown(ButtonConcept buttonConcept)
        {
            switch (buttonConcept)
            {
                case ButtonConcept.QUIT:
                    return (_KeyStates[0x1B]);
                case ButtonConcept.JUMP:
                    return (_KeyStates[0x20]);
                case ButtonConcept.FORWARD:
                    return (_KeyStates[0x26] || _KeyStates[0x57]);
                case ButtonConcept.BACKWARD:
                    return (_KeyStates[0x28] || _KeyStates[0x53]);
                case ButtonConcept.LEFT:
                    return (_KeyStates[0x25] || _KeyStates[0x41]);
                case ButtonConcept.RIGHT:
                    return (_KeyStates[0x27] || _KeyStates[0x44]);
                case ButtonConcept.FIRE:
                    return (_KeyStates[0x11] || _MouseButtonStates[0]);
                case ButtonConcept.TEST_BUTTON1:
                    return (_KeyStates[0x24]);
                case ButtonConcept.SPRINT:
                    return (_KeyStates[0x10]);
            }

            return false;
        }

        private static Boolean WasButtonPressed(ButtonConcept buttonConcept)
        {
            switch (buttonConcept)
            {
                case ButtonConcept.FIRE:
                    return ((_KeyStates[0x11] && !_PastKeyStates[0x11])) || ((_MouseButtonStates[0] && !_PastMouseButtonStates[0]));
                case ButtonConcept.TEST_BUTTON1:
                    return (_KeyStates[0x24] && !_PastKeyStates[0x24]);
            }

            return false;
        }

        public static Boolean WasButtonReleased(ButtonConcept buttonConcept)
        {
            switch (buttonConcept)
            {
                case ButtonConcept.FIRE:
                    return ((!_KeyStates[0x11] && _PastKeyStates[0x11])) || ((!_MouseButtonStates[0] && _PastMouseButtonStates[0]));
                case ButtonConcept.TEST_BUTTON1:
                    return (!_KeyStates[0x24] && _PastKeyStates[0x24]);
            }

            return false;
        }

        /// <summary>
        ///     Native interop methods. Should not be accessed outside of InputHandler!
        /// </summary>
        private static class NativeMethods
        {
            public delegate void KeyboardCallBack(Int32 key);

            public delegate void MouseActionCallBack(Int32 x);

            public delegate void MouseMoveCallBack(Int32 x, Int32 y);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerSetKeyboardDownCallback(KeyboardCallBack callback);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerSetKeyboardUpCallback(KeyboardCallBack callback);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerSetMouseMoveCallback(MouseMoveCallBack callback);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerSetMousePressCallback(MouseActionCallBack callback);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerSetMouseReleaseCallback(MouseActionCallBack callback);
        }
    }
}