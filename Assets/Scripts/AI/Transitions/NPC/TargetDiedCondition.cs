using AI.States.NPC;
using Entity;
using Game.State;

namespace AI.Transitions.NPC
{
    public class TargetDiedCondition : CharacterStateCondition
    {
        public TargetDiedCondition(Character character) : base(character)
        {
        }

        public override bool IsHappened()
        {
            var chaseState = Character.StateMachine.GetState<ChaseState>();
            var target = chaseState.Target;

            return GameStateMachine.Instance.Current is BattleGameState &&
                (target == null || target.IsAlive() == false);
        }
    }
}