using Entity;
using Interactable;
using Structure;
using UnityEngine;

namespace AI.States
{
    public class InteractState : CharacterState, IEnterState, IUpdateState, IExitState
    {
        private readonly Timer _timer;
        private readonly IInteractCharacter _interactCharacter;

        public InteractState(Character character, float duration) : base(character)
        {
            _timer = new Timer(duration);
            _interactCharacter = Context.GetComponent<IInteractCharacter>();
        }

        public void Enter()
        {
            Ended = _interactCharacter.CanInteract() == false;

            if (_interactCharacter.CanInteract())
                _interactCharacter.Interact();
        }

        public void Update()
        {
            if (_interactCharacter.CanInteract() == false)
                return;

            Ended = _timer.Tick(Time.deltaTime);
        }

        public void Exit()
        {
            Ended = false;
            _timer.Reset();

            _interactCharacter.EndInteract();
        }

        public void SetDuration(float duration)
        {
            _timer.Restart(duration);
        }
    }
}