using Stride.Core.Mathematics;
using Stride.Input;
using System.Linq;

namespace PlatformerTutorial.Characters.States
{
    public class PatrolState : CharacterState
    {
        private readonly float patrolSpeed;
        private readonly float directionChangeCooldown = 0.5f;
        private float timeSinceLastDirectionChange = 0f;

        public PatrolState(float speed)
        {
            patrolSpeed = speed;
        }

        public override void EnterState()
        {
            CurrentDirection = Direction.Left;
        }

        public override void UpdateState()
        {
            // Update the cooldown timer
            timeSinceLastDirectionChange += (float)Character.Game.UpdateTime.Elapsed.TotalSeconds;

            // Move the character based on the current direction
            Vector3 moveDirection = Vector3.Zero;
            moveDirection.X = CurrentDirection == Direction.Right ? patrolSpeed : -patrolSpeed;
            Character.CharacterComponent.SetVelocity(moveDirection);

            // Play the appropriate walking animation for the current direction
            if (CurrentDirection == Direction.Right)
            {
                Character.CurrentAnimation.PlayWalkRightAnimation(Character);
            }
            else
            {
                Character.CurrentAnimation.PlayWalkLeftAnimation(Character);
            }

            // Check for collisions and change direction if necessary
            if (IsCollidingWithWall() && timeSinceLastDirectionChange >= directionChangeCooldown)
            {
                ChangeDirection();
            }
        }

        private bool IsCollidingWithWall()
        {
            // Improved collision detection or raycasting logic can be placed here
            return Character.CharacterComponent.Collisions
                .Any(c => c.ColliderA.Entity.Name == "BigPlatform" ||
                     c.ColliderA.Entity.Name == "SmallPlatform" ||
                     c.ColliderB.Entity.Name == "BigPlatform" ||
                     c.ColliderB.Entity.Name == "SmallPlatform");
        }

        private void ChangeDirection()
        {
            // Reset the cooldown timer
            timeSinceLastDirectionChange = 0f;

            // Switch direction when a wall is detected
            CurrentDirection = CurrentDirection == Direction.Right ? Direction.Left : Direction.Right;

            // Apply a larger nudge to ensure the character moves away from the wall
            Vector3 nudge = CurrentDirection == Direction.Right ? new Vector3(0.5f, 0, 0) : new Vector3(-0.5f, 0, 0);
            Character.CharacterComponent.SetVelocity(nudge);
        }

        public override void ExitState()
        {
            // Add cleanup or exit animation.
        }
    }
}
