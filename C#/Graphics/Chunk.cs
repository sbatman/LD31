using System;

namespace LD31.Graphics
{
    /// <summary>
    /// Creates a single OpenGL Mesh out of a chunk of Blocks.
    /// 
    /// This single Mesh is then passed to the GraphicsManager for rendering.
    /// </summary>
    public class Chunk
    {
        private Block[, ,] _blocks;

        private Int32 _chunkSize;

        //private Mesh _chunkMesh

        public Chunk(Int32 chunkSize = 16)
        {
            _chunkSize = chunkSize;
            _blocks = new Block[chunkSize, chunkSize, chunkSize];

            for (Int32 x = 0; x < _chunkSize; x++)
            {
                for (Int32 y = 0; y < _chunkSize; y++)
                {
                    for (Int32 z = 0; z < _chunkSize; z++)
                    {
                        _blocks[x, y, z] = new Block();
                    }
                }
            }
        }

        public void CreateMesh()
        {
            //OpenGL->CreateMesh(ID, TYPE);

            for (Int32 x = 0; x < _chunkSize; x++)
            {
                for (Int32 y = 0; y < _chunkSize; y++)
                {
                    for (Int32 z = 0; z < _chunkSize; z++)
                    {
                        if (_blocks[x, y, z].IsActive == false)
                        {
                            continue;
                        }

                        CreateCube();
                    }
                }
            }
        }

        private void CreateCube()
        {
            //TODO: Create a single cube from the individual Blocks. Store this in the OpenGL Mesh

            //OpenGL::Renderer->AddVertexToMesh( ... )

            //OpenGL::Renderer->AddTriangleToMesh( ... )
        }

        public void Render()
        {
            GraphicsManager.StartDraw();

            // Set Position Information
            // Get Mesh ID
            // Pass Mesh to OpenGL Render for Rendering

            GraphicsManager.EndDraw();
        }
    }
}
