using AI.States;
using Entity;

namespace AI.Transitions
{
    public class StateEndedCondition<TState> : CharacterStateCondition where TState : IState
    {
        private IState State => Character.StateMachine.GetState<TState>();

        public StateEndedCondition(Character character) : base(character)
        {
        }

        public override bool IsHappened()
        {
            return State.Ended;
        }
    }
}