using AI.Navigation;
using AI.States;
using UnityEngine;

namespace Interactable.Door
{
    public class OpenedDoorState : DoorState, IEnterState<Vector2>
    {
        private readonly DoorPhysicBody _doorPhysicBody;

        public OpenedDoorState(DoorInteractable context, DoorPhysicBody doorPhysicBody) : base(context)
        {
            _doorPhysicBody = doorPhysicBody;
        }

        public void Enter(Vector2 interactPosition)
        {
            _doorPhysicBody.Open(interactPosition);
        }
    }
}