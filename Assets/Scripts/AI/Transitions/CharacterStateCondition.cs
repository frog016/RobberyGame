using AI.States;
using Entity;

namespace AI.Transitions
{
    public abstract class CharacterStateCondition : ICondition
    {
        protected Character Character;

        protected CharacterStateCondition(Character character)
        {
            Character = character;
        }

        public abstract bool IsHappened();

        public virtual void SetArgument(IState state)
        {
        }
    }
}