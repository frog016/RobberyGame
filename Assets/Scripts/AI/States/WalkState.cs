using Entity;
using System;
using UnityEngine;

namespace AI.States
{
    public class WalkState : CharacterState, IEnterState<Func<Vector2>>, IUpdateState
    {
        private Func<Vector2> _directionProvider;

        public WalkState(Character character) : base(character)
        {
        }

        public void Enter(Func<Vector2> directionProvider)
        {
            _directionProvider = directionProvider;
        }

        public void Update()
        {
            var direction = _directionProvider.Invoke();
            Context.Movement.Move(direction);
        }
    }
}