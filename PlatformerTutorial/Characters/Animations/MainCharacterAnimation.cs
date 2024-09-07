using Stride.Input;
using System;

namespace PlatformerTutorial.Characters.Animations
{
    public class MainCharacterAnimation : Animation
    {
        private const int IdleRightFrame = 14;
        private const int IdleLeftFrame = 15;
        private const int JumpRightFrame = 12;
        private const int JumpLeftFrame = 13;

        private static readonly int[] WalkRightFrames = [0, 1, 2, 3, 4];
        private static readonly int[] WalkLeftFrames = [6, 7, 8, 9, 10];


        private double animationTimer = 0f; // Timer for handling animation frame updates
        private readonly float animationInterval = 1f / 12f; // Interval between animation frames (12 fps)
        private Direction direction = Direction.Right;

        public override void PlayIdleAnimation(GameCharacter gameCharacter, Direction direction)
        {
            gameCharacter.SpriteComponent.CurrentFrame = direction == Direction.Right ? IdleRightFrame : IdleLeftFrame;
        }

        public override void PlayJumpAnimation(GameCharacter gameCharacter, Direction direction)
        {
            gameCharacter.SpriteComponent.CurrentFrame = direction == Direction.Right ? JumpRightFrame : JumpLeftFrame;
        }

        public override void PlayWalkLeftAnimation(GameCharacter gameCharacter)
        {
            UpdateAnimationFrame(gameCharacter, Direction.Left);
        }

        public override void PlayWalkRightAnimation(GameCharacter gameCharacter)
        {
            UpdateAnimationFrame(gameCharacter, Direction.Right);
        }

        protected override void UpdateAnimationFrame(GameCharacter gameCharacter, Direction direction)
        {
            animationTimer += gameCharacter.Game.UpdateTime.Elapsed.TotalSeconds;
            if (animationTimer >= animationInterval)
            {
                animationTimer = 0f;

                if (direction == Direction.Right)
                {
                    // Cycle through WalkRightFrames
                    int nextFrameIndex = (Array.IndexOf(WalkRightFrames, gameCharacter.SpriteComponent.CurrentFrame) + 1) % WalkRightFrames.Length;
                    gameCharacter.SpriteComponent.CurrentFrame = WalkRightFrames[nextFrameIndex];
                }
                else
                {
                    // Cycle through WalkLeftFrames
                    int nextFrameIndex = (Array.IndexOf(WalkLeftFrames, gameCharacter.SpriteComponent.CurrentFrame) + 1) % WalkLeftFrames.Length;
                    gameCharacter.SpriteComponent.CurrentFrame = WalkLeftFrames[nextFrameIndex];
                }
            }
        }
    }
}
