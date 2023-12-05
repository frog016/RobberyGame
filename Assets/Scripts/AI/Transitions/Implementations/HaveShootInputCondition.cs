using System;
using AI.States;
using Entity;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace AI.Transitions
{
    public class HaveShootInputCondition : CharacterStateCondition
    {
        private readonly InputAction _shootInput;
        private readonly Camera _camera;

        public HaveShootInputCondition(Character character, PlayerInput input, Camera camera) : base(character)
        {
            _shootInput = input.CharacterBattleMode.Shoot;
            _camera = camera;
        }

        public override bool IsHappened()
        {
            return _shootInput.IsPressed();
        }

        public override void SetArgument(IState state)
        {
            state.EnterState<Func<Vector2>>(GetMouseDirection);
        }

        private Vector2 GetMouseDirection()
        {
            var mousePosition = Mouse.current.position.value;
            var worldPosition = (Vector2)_camera.ScreenToWorldPoint(mousePosition);

            return (worldPosition - Character.Position).normalized;
        }
    }
}