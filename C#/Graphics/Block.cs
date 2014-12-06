using System;

namespace LD31.Graphics
{
    /// <summary>
    /// Represents a single 'Block' in our Game World
    /// </summary>
    public class Block : IDisposable
    {
        private Boolean _Active;

        private Boolean _Collidable;

        private BlockType _BlockType;

        public Block()
        {
            _BlockType = BlockType.Default;
            IsActive = true;
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

        public void Draw(Int32 x, Int32 y, Int32 z)
        {
            if (!_Active) return;
            GraphicsManager.DrawWorldVoxel(x, y, z, 255, 255, 255, 80);
        }

        public void Dispose()
        {
        }
    }
}
