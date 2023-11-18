using System;
using Entity;
using UnityEngine;

namespace AI.States
{
    public class SquatState : CharacterState, IEnterState<Func<Vector2>>, IUpdateState, IExitState
    {
        private Func<Vector2> _directionProvider;

        private readonly float _speed;

        private float _initialSpeed;

        public SquatState(Character character, float speed) : base(character)
        {
            _speed = speed;
        }

        public void Enter(Func<Vector2> directionProvider)
        {
            _directionProvider = directionProvider;

            _initialSpeed = Context.Movement.Speed;
            Context.Movement.Speed = _speed;
        }

        public void Update()
        {
            var direction = _directionProvider.Invoke();
            Context.Movement.Move(direction);
        }

        public void Exit()
        {
            Context.Movement.Speed = _initialSpeed;
        }
    }
}