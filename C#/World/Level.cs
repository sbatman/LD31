using System;

namespace LD31.Graphics
{
    /// <summary>
    /// Creates a single OpenGL Mesh out of a chunk of Blocks.
    /// 
    /// This single Mesh is then passed to the GraphicsManager for rendering.
    /// </summary>
    public class Level
    {
        public const int WORLD_BLOCK_SIZE = 32;

        private readonly Block[, ,] _Blocks;

        private readonly Int32 _ChunkSize;

        private readonly int _SizeX;
        private readonly int _SizeY;
        private readonly int _SizeZ;


        //private Mesh _chunkMesh

        public Level(Int32 sizeX, Int32 sizeZ, Int32 sizeY)
        {
            _SizeX = sizeX;
            _SizeZ = sizeZ;
            _SizeY = sizeY;
            _Blocks = new Block[sizeX, sizeZ, sizeY];

        }

        public void Render()
        {

            for (Int32 x = 0; x < _SizeX; x++)
            {
                for (Int32 z = 0; z < _SizeZ; z++)
                {
                    for (Int32 y = 0; y < _SizeY; y++)
                    {
                        if (_Blocks[x, z, y] != null)
                        {
                            _Blocks[x, z, y].Draw(x, y, z);
                        }
                    }
                }
            }

        }

        public void SetBlock(Block block, Int32 x, Int32 y, Int32 z)
        {
            if (_Blocks[x, z, y] != null) _Blocks[x, z, y].Dispose();
            _Blocks[x, z, y] = block;
        }

        public bool IsSolid(Double x, Double y, Double z)
        {
            int testPosX = (int)(x / 32);
            int testPosy = (int)(y / 32);
            int testPosz = (int)(z / 32);
            if (_Blocks[testPosX, testPosz, testPosy] == null) return false;
            return (_Blocks[testPosX, testPosz, testPosy].IsCollidable);
        }
    }
}
