using AI.FSM;
using AI.States;
using Entity;
using Entity.Attack;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable.Electricity
{
    public class ElectricalBoard : InteractableBase
    {
        [SerializeField] private ElectricalBoarStateType _initialState;

        public bool HaveElectricity => _objectStateMachine.Current is ElectricalBoardEnableState;
        public event Action<ElectricalBoarStateType> StateChanged;

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
                case ElectricalBoardEnableState:
                    TryDisableElectricity();
                    break;
                case ElectricalBoardDisableState:
                    if (character.TeamId == TeamId.Police)
                        _objectStateMachine.SetState<ElectricalBoardEnableState>();
                    break;
            }
        }

        public void RaiseStateEvent(ElectricalBoarStateType state)
        {
            StateChanged?.Invoke(state);
        }

        private void TryDisableElectricity()
        {
            _objectStateMachine.SetState<ElectricalBoardDisableState>();
            //_miniGameService.Launch<UnlockMiniGame>(
            //    () => _objectStateMachine.SetState<ElectricalBoardDisableState>(),
            //    () => Debug.Log("You couldn't disable electricity."));
        }

        private IStateMachine CreateStateMachine()
        {
            var objectStates = GetObjectStates();
            return new StateMachine(objectStates);
        }

        private IEnumerable<IState> GetObjectStates()
        {
            yield return new ElectricalBoardEnableState(this);
            yield return new ElectricalBoardDisableState(this);
        }

        private void SetInitialState()
        {
            switch (_initialState)
            {
                case ElectricalBoarStateType.Enabled:
                    _objectStateMachine.SetState<ElectricalBoardEnableState>();
                    break;
                case ElectricalBoarStateType.Disabled:
                    _objectStateMachine.SetState<ElectricalBoardDisableState>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
