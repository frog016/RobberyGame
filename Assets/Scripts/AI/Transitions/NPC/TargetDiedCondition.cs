using AI.States.NPC;
using Entity;

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

            return target != null && target.IsAlive() == false;
        }
    }
}