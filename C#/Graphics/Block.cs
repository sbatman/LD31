using System;

namespace LD31.Graphics
{
    /// <summary>
    /// Represents a single 'Block' in our Game World
    /// </summary>
    public class Block
    {
        private Boolean _active;

        private Boolean _collidable;

        private BlockType _blockType;
        
        public Block()
        {
            _blockType = BlockType.Default;

            IsActive = true;
        }

        /// <summary>
        /// Gets/Sets if the Player can pass through the Block or not
        /// </summary>
        public Boolean IsCollidable
        {
            get { return _collidable; }
            set { _collidable = value; }
        }

        /// <summary>
        /// Sets if the Block is 'Active' or not (In-Active Blocks won't be rendered)
        /// </summary>
        public Boolean IsActive
        {
            get { return _active; }
            set { _active = value; }
        }
    }
}
