using LD31.Math;

namespace LD31.Objects
{
    /// <summary>
    /// This class represents the player
    /// </summary>
    public class Player : Combatant
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public Player(Vector3 position)
            : base(position)
        {
            Graphics.GraphicsManager.SetCameraPosition(position.X, position.Y, position.Z);
        }

        /// <summary>
        /// This value represents the position of the object.
        /// </summary>
        public override Vector3 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                Graphics.GraphicsManager.SetCameraPosition(value.X, value.Y,value.Z);
            }
        }
    }
}
