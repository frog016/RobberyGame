using Entity;
using UnityEngine.InputSystem;

namespace AI.Transitions
{
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