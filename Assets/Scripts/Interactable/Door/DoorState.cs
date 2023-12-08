using AI.States;

namespace Interactable.Door
{
    public abstract class DoorState : IState
    {
        public bool Ended { get; protected set; }

        protected readonly DoorInteractable Context;

        protected DoorState(DoorInteractable context)
        {
            Context = context;
        }
    }
}