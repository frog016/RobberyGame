using Entity;
using Game.State;

namespace AI.Transitions
{
    public class BattleModeActiveCondition : CharacterStateCondition
    {
        public BattleModeActiveCondition(Character character) : base(character)
        {
        }

        public override bool IsHappened()
        {
            return GameStateMachine.Instance.Current is BattleGameState;
        }
    }
}