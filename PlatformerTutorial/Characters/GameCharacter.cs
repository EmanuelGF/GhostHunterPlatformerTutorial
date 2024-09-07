using PlatformerTutorial.Characters.Animations;
using PlatformerTutorial.Characters.Controls;
using PlatformerTutorial.Characters.States;
using Stride.Engine;
using Stride.Physics;
using Stride.Rendering.Sprites;

namespace PlatformerTutorial.Characters
{
    public class GameCharacter : SyncScript
    {
        public float Health { get; set; } = 100;
        public float Speed { get; set; } = 5.0f;
        public CharacterTypes CharacterType { get; set; }
        public ControlTypes ControlType { get; set; }
        public SpriteFromSheet SpriteComponent { get; private set; }
        public CharacterComponent CharacterComponent { get; private set; }

        public Animation CurrentAnimation { get; private set; }
        public CharacterState CurrentState { get; set; }

        public CharacterState IdleState { get; private set; }
        public CharacterState WalkingState { get; private set; }
        public CharacterState JumpingState { get; private set; }
        public CharacterState PatrolState { get; private set; }

        public IControl CurrentControl { get; private set; }

        public override void Start()
        {
            SpriteComponent = Entity.Get<SpriteComponent>().SpriteProvider as SpriteFromSheet;
            CharacterComponent = Entity.Get<CharacterComponent>();

            InitializeStates();

            CharacterConfigurator(CharacterType);
            ControlSelector(ControlType);
        }

        public override void Update()
        {
            CurrentState.UpdateState();
        }

        private void InitializeStates()
        {
            IdleState = new IdleState();
            IdleState.Initialize(this);

            WalkingState = new WalkingState();
            WalkingState.Initialize(this);

            JumpingState = new JumpingState();
            JumpingState.Initialize(this);

            PatrolState = new PatrolState(Speed);
            PatrolState.Initialize(this);
        }

        public void AttachInputHandler(IControl control)
        {
            control.OnMoveLeft += () => CurrentState.MoveLeft();
            control.OnMoveRight += () => CurrentState.MoveRight();
            control.OnJump += () => CurrentState.Jump();
            control.OnIdle += () => CurrentState.Idle();
        }
        private void ControlSelector(ControlTypes controlType)
        {
            switch (controlType)
            {
                case ControlTypes.Keyboard:
                    CurrentControl = Entity.Get<keyboardControl>();
                    AttachInputHandler(CurrentControl);
                    break;
                case ControlTypes.None:
                    // Used for automated characters.
                    break;
                default:
                    break;
            }
        }

        private void CharacterConfigurator(CharacterTypes characterType)
        {
            switch (characterType)
            {
                case CharacterTypes.MainCharacter:
                    SetAnimation(new MainCharacterAnimation());
                    CurrentState = IdleState;
                    CurrentState.EnterState();
                    break;
                case CharacterTypes.Ghost:
                    SetAnimation(new MainCharacterAnimation());
                    CurrentState = PatrolState;
                    CurrentState.EnterState();
                    break;
                case CharacterTypes.Zombie:
                    break;
                default:
                    break;
            }
        }

        private void SetAnimation(Animation animationStrategy)
        {
            CurrentAnimation = animationStrategy;
        }
    }
}
