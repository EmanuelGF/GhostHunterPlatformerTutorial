using Stride.Core.Mathematics;
using System;

namespace PlatformerTutorial.Characters.States
{
    public class IdleState : CharacterState
    {
        public override void EnterState()
        {
            Character.CharacterComponent.SetVelocity(Vector3.Zero);
            Character.CurrentAnimation.PlayIdleAnimation(Character, CurrentDirection);
        }

        public override void MoveLeft()
        {
            ChangeState(Character.WalkingState);
            Character.CurrentState.MoveLeft();
        }

        public override void MoveRight()
        {
            ChangeState(Character.WalkingState);
            Character.CurrentState.MoveRight();
        }

        public override void Jump()
        {
            ChangeState(Character.JumpingState);
            Character.CurrentState.Jump();
        }

        public override void Idle()
        {
            Character.CurrentAnimation.PlayIdleAnimation(Character, CurrentDirection);
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
