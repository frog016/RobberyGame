using Entity;
using Structure;
using UnityEngine;

namespace AI.States
{
    public class ChargeState : CharacterState, IEnterState<Vector2>, IUpdateState, IExitState
    {
        private readonly float _speed;
        private readonly Timer _timer;

        private Vector2 _direction;
        private float _initialSpeed;

        public ChargeState(Character character, float speed, float duration) : base(character)
        {
            _speed = speed;
            _timer = new Timer(duration);
        }

        public void Enter(Vector2 direction)
        {
            _direction = direction;

            _initialSpeed = Context.Movement.Speed;
            Context.Movement.Speed = _speed;
        }

        public void Update()
        {
            Ended = _timer.Tick(Time.deltaTime);

            if (Ended == false)
                Context.Movement.Move(_direction);
        }

        public void Exit()
        {
            Context.Movement.Speed = _initialSpeed;
            Ended = false;

            _timer.Reset();
        }
    }
}