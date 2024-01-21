using AI.States;
using Entity;
using InputSystem;
using System;
using UnityEngine;
using Utilities;

namespace AI.Transitions
{
    public class HaveMovementInputCondition : CharacterStateCondition
    {
        private readonly IInputAction _moveInput;

        public HaveMovementInputCondition(Character character, IPlayerInput input) : base(character)
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