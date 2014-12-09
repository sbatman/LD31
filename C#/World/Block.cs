﻿using System;
using LD31.Graphics;
using LD31.Math;

namespace LD31.World
{
    /// <summary>
    /// Represents a single 'Block' in our Game World
    /// </summary>
    public class Block : IDisposable
    {
        private Boolean _Active;

        private Boolean _Collidable;

        private BlockType _BlockType;

        private Colour _Colour;

        private static readonly Random _RND = new Random();

        public Block()
        {
            _BlockType = BlockType.DEFAULT;
            IsActive = true;
            IsCollidable = true;
            _Colour = new Colour((byte)_RND.Next(10, 255), (byte)_RND.Next(10, 255), (byte)_RND.Next(10, 255), 255);
        }

        public Block(BlockType type)
        {
            _BlockType = type;
            IsActive = true;
        }

        /// <summary>
        /// Gets/Sets if the Player can pass through the Block or not
        /// </summary>
        public Boolean IsCollidable
        {
            get { return _Collidable; }
            set { _Collidable = value; }
        }

        /// <summary>
        /// Sets if the Block is 'Active' or not (In-Active Blocks won't be rendered)
        /// </summary>
        public Boolean IsActive
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public BlockType Type
        {
            get { return _BlockType; }
            set { _BlockType = value; }
        }

        public Colour Colour
        {
            get { return _Colour; }
            set { _Colour = value; }
        }

        public void Draw(Int32 x, Int32 y, Int32 z)
        {
            if (!_Active) return;
            GraphicsManager.DrawWorldVoxel(x, y, z, _Colour);
        }

        public void Dispose()
        {
        }
    }
}
