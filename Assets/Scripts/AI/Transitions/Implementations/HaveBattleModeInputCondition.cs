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

    public class HaveInteractInputCondition : CharacterStateCondition
    {
        private readonly InputAction _interactAction;

        public HaveInteractInputCondition(Character character, PlayerInput input) : base(character)
        {
            _interactAction = input.CharacterBaseMode.Interact;
        }

        public override bool IsHappened()
        {
            return _interactAction.WasPressedThisFrame();
        }
    }

    public class HaveReloadInputCondition : CharacterStateCondition
    {
        private readonly InputAction _reloadAction;

        public HaveReloadInputCondition(Character character, PlayerInput input) : base(character)
        {
            _reloadAction = input.CharacterBattleMode.Reload;
        }

        public override bool IsHappened()
        {
            return _reloadAction.WasPressedThisFrame();
        }
    }
}