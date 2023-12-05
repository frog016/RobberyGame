using Entity;
using Structure;
using UnityEngine;

namespace AI.States
{
    public class ReloadState : CharacterState, IUpdateState, IExitState
    {
        private readonly Timer _timer;

        public ReloadState(Character character, float duration) : base(character)
        {
            _timer = new Timer(duration);
        }

        public void Update()
        {
            Ended = _timer.Tick(Time.deltaTime);

            if (Ended)
                Context.AttackBehaviour.Gun.Reload();
        }

        public void Exit()
        {
            Ended = false;
            _timer.Reset();
        }
    }
}