using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.RightsManagement;
using LD31.Math;

namespace LD31.Graphics
{
    class GraphicsManager
    {

        public static class NativeMethods
        {
            public delegate void MouseMoveCallBack(Int32 x, Int32 y);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerInit(Int32 width, Int32 height, Int32 handle);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerUpdate();

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerBeginDraw();

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerEndDraw();

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerDrawVoxel(double x, double y, double z, Byte colourR, Byte colourG, Byte colourB, Byte colourA, UInt16 sizeX, UInt16 sizeY, UInt16 sizeZ);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerSerCameraPosition(Double x, Double y, Double z);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerSetCameraRotation(Double z, Double x);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerSetMouseMoveCallback(MouseMoveCallBack callback);

        }

        private static Camera _PrimaryCamera;

        /// <summary>
        /// Used to initalise the graphics engine
        /// </summary>
        public static void Init()
        {
            Int32 handelID = Process.GetCurrentProcess().Handle.ToInt32();
            NativeMethods.GraphicsManagerInit(1440, 800, handelID);
            NativeMethods.GraphicsManagerSetMouseMoveCallback(MouseMovedCallBack);
            _PrimaryCamera = new Camera(); ;
        }

        public static void Update()
        {
            NativeMethods.GraphicsManagerUpdate();
        }

        /// <summary>
        /// Call before attempting to draw
        /// </summary>
        public static void StartDraw()
        {
            NativeMethods.GraphicsManagerBeginDraw();
        }

        /// <summary>
        /// Draws a voxel in the game world
        /// </summary>
        /// <param name="x">X Position in world space cord</param>
        /// <param name="y">Y Position in world space cord</param>
        /// <param name="z">Z Position in world space cord</param>
        /// <param name="colour">Colours used when drawing this voxel</param>
        public static void DrawWorldVoxel(Int32 x, Int32 y, Int32 z, Colour colour)
        {
            NativeMethods.GraphicsManagerDrawVoxel(x * Level.WORLD_BLOCK_SIZE, z * Level.WORLD_BLOCK_SIZE, y * Level.WORLD_BLOCK_SIZE, colour.R, colour.G, colour.B, colour.A, Level.WORLD_BLOCK_SIZE, Level.WORLD_BLOCK_SIZE, Level.WORLD_BLOCK_SIZE);
        }

        /// <summary>
        /// Draws a voxel in the game world
        /// </summary>
        /// <param name="x">X Position in world space cord</param>
        /// <param name="y">Y Position in world space cord</param>
        /// <param name="z">Z Position in world space cord</param>
        /// <param name="colour">Colours used when drawing this voxel</param>
        public static void DrawVoxel(Vector3 position, Colour colour, Vector3 scale)
        {
            NativeMethods.GraphicsManagerDrawVoxel(position.X, position.Z, position.Y, colour.R, colour.G, colour.B, colour.A, (UInt16)scale.X, (UInt16)scale.Y, (UInt16)scale.Z);
        }


        /// <summary>
        /// Call once drawing is complete
        /// </summary>
        public static void EndDraw()
        {
            _PrimaryCamera.Update();
            NativeMethods.GraphicsManagerEndDraw();
        }

        /// <summary>
        /// Used to destroy the graphics engine
        /// </summary>
        public static void Destroy()
        {

        }

        /// <summary>
        /// Sets the cameras position in world space cord
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void SetCameraPosition(Double x, Double y, Double z)
        {
            _PrimaryCamera.SetPosition(x, y, z);
        }

        /// <summary>
        /// Sets the cameras current rotation
        /// </summary>
        /// <param name="x">Used for looking up and down</param>
        /// <param name="z">Used for looking left and right</param>
        public static void SetCameraRotation(Double x, Double z)
        {
            _PrimaryCamera.Rotatate(z, x);
        }

        public static Camera GetCamera()
        {
            return _PrimaryCamera;
        }

        private static void MouseMovedCallBack(Int32 x, Int32 y)
        {
            _PrimaryCamera.Rotatate(x * 0.2f, y * 0.2f);
            Console.WriteLine("Mouse Moved Callback {0}x{1}", x, y);
        }

    }
}
