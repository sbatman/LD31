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

        private Tuple<Byte, Byte, Byte, Byte> _Colour;

        private static Random rnd =new Random();

        public Block()
        {
            _BlockType = BlockType.Default;
            IsActive = true;
            _Colour = new Tuple<byte, byte, byte, byte>((byte)rnd.Next(10, 255), (byte)rnd.Next(10, 255), (byte)rnd.Next(10, 255),255);
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
            GraphicsManager.DrawWorldVoxel(x, y, z, _Colour.Item1, _Colour.Item2, _Colour.Item3, _Colour.Item4);
        }

        public void Dispose()
        {
        }
    }
}
