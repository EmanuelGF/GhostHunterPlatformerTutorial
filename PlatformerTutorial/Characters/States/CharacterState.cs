using Stride.Input;

namespace PlatformerTutorial.Characters.States
{
    public abstract class CharacterState
    {
        protected GameCharacter Character { get; private set; }
        protected Direction CurrentDirection { get; set; } = Direction.Right;

        public void Initialize(GameCharacter character)
        {
            Character = character;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();

        // Add common state actions bellow.

        public virtual void MoveLeft()
        {
            CurrentDirection = Direction.Left;
        }

        public virtual void MoveRight()
        {
            CurrentDirection = Direction.Right;
        }

        public virtual void Jump() { }
        public virtual void Idle() { }

        protected void ChangeState(CharacterState newState)
        {
            Character.CurrentState.ExitState();
            newState.CurrentDirection = this.CurrentDirection;
            Character.CurrentState = newState;
            Character.CurrentState.EnterState();
        }
    }
}
