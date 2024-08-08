using Stride.Core.Mathematics; // Importing the Stride mathematics library for vector math operations
using Stride.Engine; // Importing the Stride engine core functionalities
using Stride.Input; // Importing the Stride input library for handling user input
using Stride.Physics; // Importing the Stride physics library
using Stride.Rendering.Sprites; // Importing the Stride sprite rendering library

namespace PlatformerTutorial
{
    public class GhostHunterControls : SyncScript
    {
        // Declared public member fields and properties will show in the game studio
        public float Speed = 5.0f; // Movement speed of the character

        private double animationTimer = 0f; // Timer for handling animation frame updates
        private readonly float animationInterval = 1f / 12f; // Interval between animation frames (12 fps)
        private bool isJumping = false; // Flag to indicate if the character is jumping
        private bool isFacingRight = true; // Flag to indicate the direction the character is facing
        private SpriteFromSheet spriteComponent; // Reference to the sprite component for handling animations
        private CharacterComponent characterComponent; // Reference to the character component for handling physics and movement
        private Vector3 platformVelocity = Vector3.Zero; // Velocity of the platform the character is on

        public override void Start()
        {
            // Initialization of the script.
            spriteComponent = Entity.Get<SpriteComponent>().SpriteProvider as SpriteFromSheet;
            characterComponent = Entity.Get<CharacterComponent>();
        }

        public override void Update()
        {
            // Do stuff every new frame
            var moveDirection = Vector3.Zero; // Vector to store the movement direction
            bool isMoving = false; // Flag to indicate if the character is moving

            // Move right
            if (Input.IsKeyDown(Keys.Right))
            {
                moveDirection.X += Speed; // Move right by adding to the X component
                isMoving = true;
                if (!isFacingRight)
                {
                    isFacingRight = true;
                    spriteComponent.CurrentFrame = 0; // Reset to the first frame of the walking right animation
                }
            }

            // Move left
            if (Input.IsKeyDown(Keys.Left))
            {
                moveDirection.X -= Speed; // Move left by subtracting from the X component
                isMoving = true;
                if (isFacingRight)
                {
                    isFacingRight = false;
                    spriteComponent.CurrentFrame = 6; // Reset to the first frame of the walking left animation
                }
            }

            // Jump
            if (Input.IsKeyDown(Keys.Space) && characterComponent.IsGrounded)
            {
                isJumping = true;
                spriteComponent.CurrentFrame = isFacingRight ? 12 : 13; // Set the appropriate jump frame
                characterComponent.Jump(); // Make the character jump
            }

            // Apply movement, combined with platform velocity if on a platform
            characterComponent.SetVelocity(moveDirection + platformVelocity);
            UpdateAnimationFrame(isMoving);

            // Setting the default character position (idle animation).
            if (!isMoving && isFacingRight && characterComponent.IsGrounded) spriteComponent.CurrentFrame = 14; // Idle facing right
            if (!isMoving && !isFacingRight && characterComponent.IsGrounded) spriteComponent.CurrentFrame = 15; // Idle facing left

            // Check if character is grounded
            if (characterComponent.IsGrounded)
            {
                isJumping = false;
            }
        }

        private void UpdateAnimationFrame(bool isMoving)
        {
            // Update animation frame
            animationTimer += Game.UpdateTime.Elapsed.TotalSeconds;
            if (animationTimer >= animationInterval)
            {
                animationTimer = 0f;

                if (isMoving && !isJumping)
                {
                    if (isFacingRight)
                    {
                        // Cycling through frames 0 to 5 for walking right
                        spriteComponent.CurrentFrame = (spriteComponent.CurrentFrame + 1) % 5;
                    }
                    else
                    {
                        // Cycling through frames 6 to 11 for walking left
                        spriteComponent.CurrentFrame = 6 + (spriteComponent.CurrentFrame + 1) % 5;
                    }
                }
            }
        }
    }
}
