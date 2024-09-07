using Stride.Input;

namespace PlatformerTutorial.Characters.Animations
{
    public abstract class Animation
    {
        public virtual void PlayJumpAnimation(GameCharacter gameCharacter, Direction direction) { }
        public virtual void PlayWalkRightAnimation(GameCharacter gameCharacter) { }
        public virtual void PlayWalkLeftAnimation(GameCharacter gameCharacter) { }
        public virtual void PlayIdleAnimation(GameCharacter gameCharacter, Direction direction) { }


        protected abstract void UpdateAnimationFrame(GameCharacter gameCharacter, Direction direction);
    }
}
