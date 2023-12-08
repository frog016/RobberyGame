using AI.States;

namespace Interactable.Door
{
    public class ClosedDoorState : DoorState, IEnterState, IExitState
    {
        public ClosedDoorState(DoorInteractable context) : base(context)
        {
        }

        public void Enter()
        {
            Context.BlockCollider.isTrigger = false;
        }

        public void Exit()
        {
            Context.BlockCollider.isTrigger = true;
        }
    }
}