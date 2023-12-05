using Entity;
using UnityEngine.InputSystem;

namespace AI.Transitions
{
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
}