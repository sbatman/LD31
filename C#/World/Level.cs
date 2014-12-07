using System;
using System.Security.RightsManagement;

namespace LD31.Graphics
{
    /// <summary>
    /// Creates a single OpenGL Mesh out of a chunk of Blocks.
    /// 
    /// This single Mesh is then passed to the GraphicsManager for rendering.
    /// </summary>
    public class Level : IDisposable
    {
        public const int WORLD_BLOCK_SIZE = 32;
        public const Double GRAVITY = 0.98 * 6;

        private Block[, ,] _Blocks;

        private readonly int _SizeX;
        private readonly int _SizeY;
        private readonly int _SizeZ;

        //private Mesh _chunkMesh

        public Level(Int32 sizeX, Int32 sizeY, Int32 sizeZ)
        {
            _SizeX = sizeX;
            _SizeY = sizeY;
            _SizeZ = sizeZ;
            _Blocks = new Block[sizeX, sizeY, sizeZ];
        }

        public void Render()
        {
            for (Int32 x = 0; x < _SizeX; x++)
            {
                for (Int32 y = 0; y < _SizeY; y++)
                {
                    for (Int32 z = 0; z < _SizeZ; z++)
                    {
                        if (_Blocks[x, y, z] != null)
                        {
                            _Blocks[x, y, z].Draw(x, y, z);
                        }
                    }
                }
            }

        }

        public void SetBlock(Block block, Int32 x, Int32 y, Int32 z)
        {
            if (_Blocks[x, y, z] != null) _Blocks[x, y, z].Dispose();
            _Blocks[x, y, z] = block;
        }

        public Block GetBlock(Double x, Double y, Double z)
        {
            int testPosX = (int)System.Math.Round(x / 32.0);
            int testPosY = (int)System.Math.Round(y / 32.0);
            int testPosZ = (int)System.Math.Round(z / 32.0);
            return _Blocks[testPosX, testPosY, testPosZ];
        }

        public bool IsSolid(Double x, Double y, Double z)
        {
            int testPosX = (int)System.Math.Round(x / 32);
            int testPosY = (int)System.Math.Round(y / 32);
            int testPosZ = (int)System.Math.Round(z / 32);

            if (testPosX >= _Blocks.GetLength(0) || testPosX < 0) return false;
            if (testPosY >= _Blocks.GetLength(1) || testPosY < 0) return false;
            if (testPosZ >= _Blocks.GetLength(2) || testPosZ < 0) return false;


            if (_Blocks[testPosX, testPosY, testPosZ] == null) return false;
            return (_Blocks[testPosX, testPosY, testPosZ].IsCollidable);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            for (Int32 x = 0; x < _SizeX; x++)
            {
                for (Int32 y = 0; y < _SizeY; y++)
                {
                    for (Int32 z = 0; z < _SizeZ; z++)
                    {
                        if (_Blocks[x, y, z] != null)
                        {
                            _Blocks[x, y, z].Dispose();
                        }
                    }
                }
            }
            _Blocks = null;
        }
    }
}
