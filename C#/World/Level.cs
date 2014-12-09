using System;
using System.IO;
using LD31.Math;
using LD31.Objects;

namespace LD31.World
{
    public class Level : IDisposable
    {
        public const int WORLD_BLOCK_SIZE = 32;
        public const Double GRAVITY = 0.98*6;
        private Block[,,] _Blocks;
        private readonly int _SizeX;
        private readonly int _SizeY;
        private readonly int _SizeZ;

        public Level(Int32 sizeX, Int32 sizeY, Int32 sizeZ)
        {
            _SizeX = sizeX;
            _SizeY = sizeY;
            _SizeZ = sizeZ;
            _Blocks = new Block[sizeX, sizeY, sizeZ];
        }

        public Level(String levelFile)
        {
            _Blocks = new Block[64, 64, 32];


            _SizeX = 64;
            _SizeY = 64;
            _SizeZ = 32;

            using (StreamReader sReader = new StreamReader(levelFile))
            {
                while (!sReader.EndOfStream)
                {
                    string line = sReader.ReadLine();
                    if (line == null) continue;
                    String[] vertexData = line.Split(' ');
                    Int32 x = int.Parse(vertexData[0]);
                    Int32 z = int.Parse(vertexData[1]);
                    Int32 y = int.Parse(vertexData[2]);
                    Int32 t = int.Parse(vertexData[3]);
                    if (t == 4)
                    {
                        Enemy.SpawnLocations.Add(new Vector3(x*32, y*32, z*32));
                        continue;
                    }

                    _Blocks[x, y, z] = new Block();
                    switch (t)
                    {
                        case 9:
                            _Blocks[x, y, z].Colour = new Colour(30, 30, 30, 255);
                            break;
                        case 8:
                            _Blocks[x, y, z].Colour = new Colour(40, 40, 60, 255);
                            break;
                        case 0:
                            _Blocks[x, y, z].Colour = new Colour(255, 0, 0, 255);
                            break;
                        case 3:
                            _Blocks[x, y, z].Colour = new Colour(255, 255, 0, 255);
                            break;
                        case 5:
                            _Blocks[x, y, z].Colour = new Colour(120, 120, 120, 255);
                            break;
                    }

                    //TODO: Populate the array with the data
                }
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
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
            int testPosX = (int) System.Math.Round(x/32.0);
            int testPosY = (int) System.Math.Round(y/32.0);
            int testPosZ = (int) System.Math.Round(z/32.0);
            return _Blocks[testPosX, testPosY, testPosZ];
        }

        public bool IsSolid(Double x, Double y, Double z)
        {
            int testPosX = (int) System.Math.Round(x/32);
            int testPosY = (int) System.Math.Round(y/32);
            int testPosZ = (int) System.Math.Round(z/32);

            if (testPosX >= _Blocks.GetLength(0) || testPosX < 0) return false;
            if (testPosY >= _Blocks.GetLength(1) || testPosY < 0) return false;
            if (testPosZ >= _Blocks.GetLength(2) || testPosZ < 0) return false;


            if (_Blocks[testPosX, testPosY, testPosZ] == null) return false;
            return (_Blocks[testPosX, testPosY, testPosZ].IsCollidable);
        }
    }
}