using System;
using LD31.Graphics;
using LD31.Input;
using LD31.Math;

namespace LD31.Objects
{
    /// <summary>
    ///     This class represents the player
    /// </summary>
    public class Player : Combatant
    {
        /// <summary>
        ///     CTOR
        /// </summary>
        public Player(Vector3 position)
            : base(position)
        {
            GraphicsManager.SetCameraPosition(position.X, position.Y, position.Z);
        }

        /// <summary>
        ///     This value represents the position of the object.
        /// </summary>
        public override Vector3 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                GraphicsManager.SetCameraPosition(value.X, value.Y, value.Z);
            }
        }

        /// <summary>
        ///     The update method of the player class.
        /// </summary>
        /// <param name="msSinceLastUpdate"></param>
        public override void Update(Double msSinceLastUpdate)
        {
            _JumpCoolDown -= msSinceLastUpdate;
            if (InputHandler.IsButtonDown(ButtonConcept.JUMP) && IsOnFloor() && _JumpCoolDown <= 0)
            {
                Velocity.Z += 4;
                _JumpCoolDown = 250;
            }

            if (InputHandler.IsButtonDown(ButtonConcept.FORWARD) && IsOnFloor())
            {
                double speedMultiplier = InputHandler.IsButtonDown(ButtonConcept.SPRINT) ? 1.5 : 1;
                Vector2 movement = Vector2.Rotate(new Vector2(0, -0.2*speedMultiplier), GraphicsManager.GetCamera().RotationZ);
                Velocity.X += movement.X;
                Velocity.Y += movement.Y;
            }

            if (InputHandler.IsButtonDown(ButtonConcept.BACKWARD) && IsOnFloor())
            {
                Vector2 movement = Vector2.Rotate(new Vector2(0, 0.2), GraphicsManager.GetCamera().RotationZ);
                Velocity.X += movement.X;
                Velocity.Y += movement.Y;
            }

            if (InputHandler.IsButtonDown(ButtonConcept.LEFT) && IsOnFloor())
            {
                Vector2 movement = Vector2.Rotate(new Vector2(-0.2, 0), GraphicsManager.GetCamera().RotationZ);
                Velocity.X += movement.X;
                Velocity.Y += movement.Y;
            }

            if (InputHandler.IsButtonDown(ButtonConcept.RIGHT) && IsOnFloor())
            {
                Vector2 movement = Vector2.Rotate(new Vector2(0.2, 0), GraphicsManager.GetCamera().RotationZ);
                Velocity.X += movement.X;
                Velocity.Y += movement.Y;
            }

            if (InputHandler.WasButtonReleased(ButtonConcept.FIRE))
            {
                Console.WriteLine("Bang");

                CurrentWeapon.Fire();
            }

            base.Update(msSinceLastUpdate);
        }
    }
}