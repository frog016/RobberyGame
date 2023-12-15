using AI.States;
using AI.States.NPC;
using Entity;
using Utilities;

namespace AI.Transitions.NPC
{
    public class ReadyToPatrolCondition: CharacterStateCondition
    {
        private readonly PatrolWayPoint[] _wayPoints;

        public ReadyToPatrolCondition(Character character, PatrolWayPoint[] wayPoints) : base(character)
        {
            _wayPoints = wayPoints;
        }

        public override bool IsHappened()
        {
            return Character.StateMachine.Current is IdleState;
        }

        public override void SetArgument(IState state)
        {
            state.EnterState(_wayPoints);
        }
    }
}