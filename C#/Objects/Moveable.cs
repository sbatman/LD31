using LD31.Math;

namespace LD31.Objects
{
    /// <summary>
    /// This class represents a game object that can be moved.
    /// </summary>
    public abstract class Moveable : GameObject
    {
        /// <summary>
        /// Private backing field for position
        /// </summary>
        private Vector3 _Position;

        /// <summary>
        /// Backing field for velocity
        /// </summary>
        private Vector3 _Velocity;

        /// <summary>
        /// CTOR
        /// </summary>
        public Moveable(Vector3 position)
        {
            _Position = position;
            _Velocity = new Vector3(0);
        }

        /// <summary>
        /// This value represents the position of the object.
        /// </summary>
        public virtual Vector3 Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        /// <summary>
        /// This value represents the position of the object.
        /// </summary>
        public virtual Vector3 Velocity
        {
            get { return _Velocity; }
            set { _Velocity = value; }
        }

        /// <summary>
        /// This function moves the object.
        /// </summary>
        /// <param name="movement"></param>
        public virtual void Move(Vector3 movement)
        {
            _Position.X += movement.X;
            _Position.Y += movement.Y;
        }

        /// <summary>
        /// Adds to the moveables velocity
        /// </summary>
        /// <param name="impulse"></param>
        public virtual void ApplyImpulse(Vector3 impulse)
        {
            _Velocity.X += impulse.X;
            _Velocity.Y += impulse.Y;
        }
    }
}
