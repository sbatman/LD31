using LD31.Math;

namespace LD31.Objects
{
    /// <summary>
    /// This class represents an enemy for the player.
    /// </summary>
    public class Enemy : Combatant
    {
         /// <summary>
        /// CTOR
        /// </summary>
        public Enemy(Vector3 position)
            : base(position)
        {
        }
    }
}
