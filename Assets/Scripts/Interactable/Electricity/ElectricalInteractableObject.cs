using System;
using UnityEngine;

namespace Interactable.Electricity
{
    public abstract class ElectricalInteractableObject : InteractableBase
    {
        [SerializeField] private ElectricalBoard _electricalBoard;

        protected bool HaveElectricity => _electricalBoard.HaveElectricity;

        protected virtual void OnEnable()
        {
            _electricalBoard.StateChanged += OnStateChanged;
        }

        protected virtual void OnDisable()
        {
            _electricalBoard.StateChanged -= OnStateChanged;
        }

        protected virtual void OnElectricityEnabled()
        {
            enabled = true;
        }

        protected virtual void OnElectricityDisabled()
        {
            enabled = false;
        }

        private void OnStateChanged(ElectricalBoarStateType state)
        {
            switch (state)
            {
                case ElectricalBoarStateType.Enabled:
                    OnElectricityEnabled();
                    break;
                case ElectricalBoarStateType.Disabled:
                    OnElectricityDisabled();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}