using Entity;

namespace AI.States
{
    public class CharacterState : IState
    {
        public bool Ended { get; protected set; }

        protected readonly Character Context;

        public CharacterState(Character context)
        {
            Context = context;
        }
    }
}