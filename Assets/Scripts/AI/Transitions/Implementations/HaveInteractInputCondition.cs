using Entity;
using InputSystem;

namespace AI.Transitions
{
    public class HaveInteractInputCondition : CharacterStateCondition
    {
        private readonly IInputAction _interactAction;

        public HaveInteractInputCondition(Character character, IPlayerInput input) : base(character)
        {
            _interactAction = input.CharacterBaseMode.Interact;
        }

        public override bool IsHappened()
        {
            return _interactAction.WasPressedThisFrame();
        }
    }
}