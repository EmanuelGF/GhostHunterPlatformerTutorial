using Stride.Core.Mathematics;
using Stride.Input;
using System;


namespace PlatformerTutorial.Characters.States
{
    public class WalkingState : CharacterState
    {
        public override void EnterState()
        {
            
        }

        public override void MoveLeft()
        {
            CurrentDirection = Direction.Left;
            Vector3 moveDirection = Vector3.Zero;
            moveDirection.X -= Character.Speed;
            Character.CharacterComponent.SetVelocity(moveDirection);
            Character.CurrentAnimation.PlayWalkLeftAnimation(Character);
        }

        public override void MoveRight()
        {
            CurrentDirection = Direction.Right;
            Vector3 moveDirection = Vector3.Zero;
            moveDirection.X += Character.Speed;
            Character.CharacterComponent.SetVelocity(moveDirection);
            Character.CurrentAnimation.PlayWalkRightAnimation(Character);
        }

        public override void Jump()
        {
            ChangeState(Character.JumpingState);
            Character.CurrentState.Jump();
        }

        public override void Idle()
        {
            ChangeState(Character.IdleState);
        }

        public override void ExitState()
        {
            // Add cleanup or exit animation.
        }

        public override void UpdateState()
        {
           
        }
    }
}
