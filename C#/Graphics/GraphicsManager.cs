using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Graphics
{
    class GraphicsManager
    {
        /// <summary>
        /// Used to initalise the graphics engine
        /// </summary>
        public static void Init()
        {

        }

        /// <summary>
        /// Call before attempting to draw
        /// </summary>
        public static void StartDraw()
        {
            
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

        }

        /// <summary>
        /// Call once drawing is complete
        /// </summary>
        public static void EndDraw()
        {

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
