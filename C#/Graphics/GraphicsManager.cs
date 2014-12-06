using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Graphics
{
    class GraphicsManager
    {

        static class NativeMethods
        {
            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerInit(Int32 width, Int32 height, Int32 handle);

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerUpdate();

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerBeginDraw();

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerEndDraw();

            [DllImport("Renderer.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern void GraphicsManagerDrawVoxel(double x, double y, double z, Byte colourR, Byte colourG, Byte colourB, Byte alpha, UInt16 sizeX, UInt16 sizeY, UInt16 sizeZ);

        }

        /// <summary>
        /// Used to initalise the graphics engine
        /// </summary>
        public static void Init()
        {
            Int32 handelID = Process.GetCurrentProcess().Handle.ToInt32();
            NativeMethods.GraphicsManagerInit(1440, 800, handelID);
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
        /// <param name="colourR">Colours red component</param>
        /// <param name="colourG">Colours green component</param>
        /// <param name="colourB">Colours blue component</param>
        public static void DrawWorldVoxel(Int32 x, Int32 y, Int32 z, Byte colourR, Byte colourG, Byte colourB, Byte alpha = 255)
        {
            NativeMethods.GraphicsManagerDrawVoxel(x * 32, y * 32, z * 32, colourR, colourG, colourB, alpha, 32, 32, 32);
        }

        /// <summary>
        /// Call once drawing is complete
        /// </summary>
        public static void EndDraw()
        {
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

        }

        /// <summary>
        /// Sets the cameras current rotation
        /// </summary>
        /// <param name="x">Used for looking up and down</param>
        /// <param name="z">Used for looking left and right</param>
        public static void SetCameraRotation(Double x, Double z)
        {

        }

    }
}
