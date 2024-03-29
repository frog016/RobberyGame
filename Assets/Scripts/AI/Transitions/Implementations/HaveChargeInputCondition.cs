﻿using AI.States;
using Entity;
using InputSystem;
using Utilities;

namespace AI.Transitions
{
    public class HaveChargeInputCondition : CharacterStateCondition
    {
        private readonly IInputAction _chargeInput;

        public HaveChargeInputCondition(Character character, IPlayerInput input) : base(character)
        {
            _chargeInput = input.CharacterBaseMode.Charge;
        }

        public override bool IsHappened()
        {
            return _chargeInput.WasPressedThisFrame();
        }

        public override void SetArgument(IState state)
        {
            state.EnterState(Character.Rotation);
        }
    }
}