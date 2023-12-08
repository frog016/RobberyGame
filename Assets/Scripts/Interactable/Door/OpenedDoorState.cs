using AI.States;

namespace Interactable.Door
{
    public class OpenedDoorState : DoorState, IEnterState, IExitState
    {
        public OpenedDoorState(DoorInteractable context) : base(context)
        {
        }

        public void Enter()
        {
            Context.BlockCollider.isTrigger = true;
        }

        public void Exit()
        {
            Context.BlockCollider.isTrigger = false;
        }
    }
}