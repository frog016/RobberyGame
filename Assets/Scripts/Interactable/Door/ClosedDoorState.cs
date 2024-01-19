using AI.Navigation;
using AI.States;

namespace Interactable.Door
{
    public class ClosedDoorState : DoorState, IEnterState
    {
        private readonly DoorPhysicBody _doorPhysicBody;

        public ClosedDoorState(DoorInteractable context, DoorPhysicBody doorPhysicBody) : base(context)
        {
            _doorPhysicBody = doorPhysicBody;
        }

        public void Enter()
        {
            _doorPhysicBody.Close();
        }
    }
}