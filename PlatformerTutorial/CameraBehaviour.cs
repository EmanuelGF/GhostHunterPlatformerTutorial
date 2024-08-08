using Stride.Core.Mathematics;
using Stride.Engine;

namespace PlatformerTutorial.Camera
{
    public class CameraBehaviour : SyncScript
    {
        // Public member fields and properties that will be shown in the game studio
        public Entity EntityToFollow; // The entity that the camera will follow
        public Vector3 Offset = new Vector3(0, 2f, 10); // The offset from the entity's position to the camera's position
        public float FollowSpeed = 3.0f; // The speed at which the camera follows the entity

        public override void Start()
        {
            // Initialization of the script. This method is called once when the script starts.
        }

        public override void Update()
        {
            // This method is called every frame
            if (EntityToFollow == null) // Check if there is an entity to follow
                return; // If not, exit the method

            var deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds; // Get the elapsed time since the last frame
            var currentPosition = Entity.Transform.Position; // Get the current position of the camera

            // Calculate the target position based on the entity's position and the offset
            var targetPosition = EntityToFollow.Transform.Position + Offset;

            // Smoothly interpolate the camera's position towards the target position
            Entity.Transform.Position = Vector3.Lerp(currentPosition, targetPosition, deltaTime * FollowSpeed);
        }
    }
}
