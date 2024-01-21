using Entity;
using InputSystem;

namespace AI.Transitions
{
    public class HaveReloadInputCondition : CharacterStateCondition
    {
        private readonly IInputAction _reloadAction;

        public HaveReloadInputCondition(Character character, IPlayerInput input) : base(character)
        {
            _reloadAction = input.CharacterBattleMode.Reload;
        }

        public override bool IsHappened()
        {
            return _reloadAction.WasPressedThisFrame();
        }
    }
}