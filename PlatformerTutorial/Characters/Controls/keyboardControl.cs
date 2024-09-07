using Stride.Engine;
using Stride.Input;
using System;


namespace PlatformerTutorial.Characters.Controls
{
    public class keyboardControl : SyncScript, IControl
    {
        public event Action OnMoveLeft;
        public event Action OnMoveRight;
        public event Action OnJump;
        public event Action OnIdle;
        public override void Update()
        {
            HandleInput();
        }

        public void HandleInput()
        {
            if (Input.IsKeyDown(Keys.Left))
            {
                OnMoveLeft.Invoke();
            }

            if (Input.IsKeyDown(Keys.Right))
            {
                OnMoveRight.Invoke();
            }

            if (Input.IsKeyDown(Keys.Space))
            {
                OnJump.Invoke();
            }

            if (Input.IsKeyReleased(Keys.Right) || Input.IsKeyReleased(Keys.Left))
            {
                OnIdle.Invoke();
            }
        }

    }
}
