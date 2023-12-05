using Entity;
using UnityEngine.InputSystem;

namespace AI.Transitions
{
    public class HaveBattleModeInputCondition : CharacterStateCondition
    {
        private readonly InputAction _launchModeAction;

        public HaveBattleModeInputCondition(Character character, PlayerInput input) : base(character)
        {
            _launchModeAction = input.CharacterStealthMode.BattleMode;
        }

        public override bool IsHappened()
        {
            return _launchModeAction.WasPressedThisFrame();
        }
    }
}