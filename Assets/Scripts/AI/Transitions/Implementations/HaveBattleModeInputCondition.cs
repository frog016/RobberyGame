using Entity;
using Game.State;
using InputSystem;

namespace AI.Transitions
{
    public class HaveBattleModeInputCondition : CharacterStateCondition
    {
        private readonly IInputAction _launchModeAction;

        public HaveBattleModeInputCondition(Character character, IPlayerInput input) : base(character)
        {
            _launchModeAction = input.CharacterStealthMode.BattleMode;
        }

        public override bool IsHappened()
        {
            return GameStateMachine.Instance.Current is not BattleGameState &&
                   _launchModeAction.WasPressedThisFrame();
        }
    }
}