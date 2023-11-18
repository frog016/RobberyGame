using System;
using AI.States;
using Entity;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace AI.Transitions
{
    public class HaveMovementInputCondition : CharacterStateCondition
    {
        private readonly InputAction _moveInput;

        public HaveMovementInputCondition(Character character, PlayerInput input) : base(character)
        {
            _moveInput = input.CharacterBaseMode.Move;
        }

        public override bool IsHappened()
        {
            return _moveInput.ReadValue<Vector2>() != default;
        }

        public override void SetArgument(IState state)
        {
            state.EnterState<Func<Vector2>>(() => _moveInput.ReadValue<Vector2>());
        }
    }
}