using System;
using System.Collections.Generic;
using AI.FSM;
using AI.States;
using Entity;
using UnityEngine;

namespace Interactable.Door
{
    public class DoorInteractable : InteractableBase
    {
        [field: SerializeField] public Collider2D BlockCollider { get; private set; }

        [SerializeField] private DoorStateType _initialState;

        private IStateMachine _objectStateMachine;

        protected override void OnServerNetworkSpawn()
        {
            _objectStateMachine = CreateStateMachine();
            SetInitialState();
        }

        public override void Interact(Character character)
        {
            switch (_objectStateMachine.Current)
            {
                case OpenedDoorState:
                    _objectStateMachine.SetState<ClosedDoorState>();
                    break;
                case ClosedDoorState:
                    _objectStateMachine.SetState<OpenedDoorState>();
                    break;
                case LockedDoorState:
                    TryUnlockDoor(character);
                    break;
            }
        }

        private void TryUnlockDoor(Character character)
        {
            //if (character.HaveKey())
            //    _objectStateMachine.SetState<OpenedDoorState>();
            //else
            //    _miniGameService.Launch<UnlockMiniGame>(
            //        () => _objectStateMachine.SetState<OpenedDoorState>(),
            //        () => Debug.Log("You couldn't break down the door."));
        }

        private IStateMachine CreateStateMachine()
        {
            var objectStates = GetObjectStates();
            return new StateMachine(objectStates);
        }

        private IEnumerable<IState> GetObjectStates()
        {
            yield return new OpenedDoorState(this);
            yield return new ClosedDoorState(this);
            yield return new LockedDoorState(this);
        }

        private void SetInitialState()
        {
            switch (_initialState)
            {
                case DoorStateType.Opened:
                    _objectStateMachine.SetState<OpenedDoorState>();
                    break;
                case DoorStateType.Closed:
                    _objectStateMachine.SetState<ClosedDoorState>();
                    break;
                case DoorStateType.Locked:
                    _objectStateMachine.SetState<LockedDoorState>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}