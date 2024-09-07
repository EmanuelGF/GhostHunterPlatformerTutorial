using System;

namespace PlatformerTutorial.Characters.Controls
{
    // Add controller events here.
    public interface IControl
    {
        event Action OnMoveLeft;
        event Action OnMoveRight;
        event Action OnJump;
        event Action OnIdle;

        void HandleInput();
    }
}
