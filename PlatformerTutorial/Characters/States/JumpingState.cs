using Stride.Core.Mathematics;
using Stride.Input;
using System;

namespace PlatformerTutorial.Characters.States
{
    public class JumpingState : CharacterState
    {
        public override void EnterState()
        {
            Character.CurrentAnimation.PlayJumpAnimation(Character, CurrentDirection);
            Character.CharacterComponent.Jump();
        }

        public override void MoveLeft()
        {
            CurrentDirection = Direction.Left;
            Vector3 moveDirection = Vector3.Zero;
            moveDirection.X -= Character.Speed;
            Character.CharacterComponent.SetVelocity(moveDirection);

            if (!Character.CharacterComponent.IsGrounded)
            {
                Character.CurrentAnimation.PlayJumpAnimation(Character, CurrentDirection);
            }
        }

        public override void MoveRight()
        {
            CurrentDirection = Direction.Right;
            Vector3 moveDirection = Vector3.Zero;
            moveDirection.X += Character.Speed;
            Character.CharacterComponent.SetVelocity(moveDirection);

            if (!Character.CharacterComponent.IsGrounded)
            {
                Character.CurrentAnimation.PlayJumpAnimation(Character, CurrentDirection);
            }
        }

        public override void Idle()
        {
            if (Character.CharacterComponent.IsGrounded)
            {
                ChangeState(Character.IdleState);
            }
        }

        public override void UpdateState()
        {
            if (Character.CharacterComponent.IsGrounded)
            {
                ChangeState(Character.IdleState);
            }
        }

        public override void Jump()
        {
            // Possible allow for a double jump.
        }

        public override void ExitState()
        {
            // Add cleanup or exit animation.
        }
    }
}
