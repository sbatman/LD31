using System;
using System.Windows.Input;
using LD31.Input;
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
                Graphics.GraphicsManager.SetCameraPosition(value.X, value.Y, value.Z);
            }
        }

        /// <summary>
        /// The update method of the player class.
        /// </summary>
        /// <param name="msSinceLastUpdate"></param>
        public override void Update(Double msSinceLastUpdate)
        {
            if (Input.InputHandler.IsButtonDown(ButtonConcept.Jump) && IsOnFloor())
            {
                Velocity.Z += 1;
            }

            if (Input.InputHandler.IsButtonDown(ButtonConcept.Forward) && IsOnFloor())
            {
                Vector2 movement = Vector2.Rotate(new Vector2(0, -0.2), Graphics.GraphicsManager.GetCamera().RotationZ);
                Velocity.X += movement.X;
                Velocity.Y += movement.Y;
            }

            if (Input.InputHandler.IsButtonDown(ButtonConcept.Backward) && IsOnFloor())
            {
                Vector2 movement = Vector2.Rotate(new Vector2(0, 0.2), Graphics.GraphicsManager.GetCamera().RotationZ);
                Velocity.X += movement.X;
                Velocity.Y += movement.Y;
            }

            if (Input.InputHandler.IsButtonDown(ButtonConcept.Left) && IsOnFloor())
            {
                Vector2 movement = Vector2.Rotate(new Vector2(-0.2, 0), Graphics.GraphicsManager.GetCamera().RotationZ);
                Velocity.X += movement.X;
                Velocity.Y += movement.Y;
            }

            if (Input.InputHandler.IsButtonDown(ButtonConcept.Right) && IsOnFloor())
            {
                Vector2 movement = Vector2.Rotate(new Vector2(0.2, 0), Graphics.GraphicsManager.GetCamera().RotationZ);
                Velocity.X += movement.X;
                Velocity.Y += movement.Y;
            }
            base.Update(msSinceLastUpdate);
        }
    }
}
