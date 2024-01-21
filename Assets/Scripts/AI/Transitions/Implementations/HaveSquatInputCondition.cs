using AI.States;
using Entity;
using InputSystem;
using System;
using UnityEngine;
using Utilities;

namespace AI.Transitions
{
    public class HaveSquatInputCondition : CharacterStateCondition
    {
        private readonly IInputAction _squatAction;
        private readonly IInputAction _moveAction;

        public HaveSquatInputCondition(Character character, IPlayerInput input) : base(character)
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