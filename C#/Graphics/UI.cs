using LD31.Math;
using System;

namespace LD31.Graphics
{
    internal class UI
    {
        public static void Draw()
        {
            for (int x = -3; x <= 3; x++) GraphicsManager.DrawUIVoxel(new Vector3(x, -200, 0), new Colour(0, 255), new Vector3(1));
            for (int y = -3; y <= 3; y++) GraphicsManager.DrawUIVoxel(new Vector3(0, -200, y), new Colour(0, 255), new Vector3(1));
        }

        public static void DrawTextToScreen(String str, Int32 offsetX, Int32 offsetY)
        {
            GraphicsManager.DrawTextToScreen(str, offsetX, offsetY);
        }
    }
}