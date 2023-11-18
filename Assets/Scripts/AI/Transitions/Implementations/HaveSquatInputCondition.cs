using System;
using AI.States;
using Entity;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace AI.Transitions
{
    public class HaveSquatInputCondition : CharacterStateCondition
    {
        private readonly InputAction _squatAction;
        private readonly InputAction _moveAction;

        public HaveSquatInputCondition(Character character, PlayerInput input) : base(character)
        {
            _squatAction = input.CharacterStealthMode.Squat;
            _moveAction = input.CharacterBaseMode.Move;
        }

        public override bool IsHappened()
        {
            return _squatAction.WasPressedThisFrame();
        }

        public override void SetArgument(IState state)
        {
            state.EnterState<Func<Vector2>>(() => _moveAction.ReadValue<Vector2>());
        }
    }
}