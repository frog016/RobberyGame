using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace InputSystem
{
    public abstract class NetworkPlayerInput : NetworkBehaviour, IPlayerInput
    {
        public BaseInputModule CharacterBaseMode { get; protected set; }
        public StealthInputModule CharacterStealthMode { get; protected set; }
        public BattleInputModule CharacterBattleMode { get; protected set; }

        private Dictionary<string, IInputAction> _inputActionTable;

        public override void OnNetworkSpawn()
        {
            InitializeInput();
            _inputActionTable = InitializeInputTable();
        }

        private void Update()
        {
            if (IsSpawned == false || IsClient == false || OwnerClientId != NetworkManager.LocalClientId)
                return;

            UpdateInputModules();
        }

        protected abstract void InitializeInput();

        private Dictionary<string, IInputAction> InitializeInputTable()
        {
            return new Dictionary<string, IInputAction>
            {
                { nameof(CharacterBaseMode.Interact), CharacterBaseMode.Interact },
                { nameof(CharacterBaseMode.Move), CharacterBaseMode.Move },
                { nameof(CharacterBaseMode.Charge), CharacterBaseMode.Charge},

                { nameof(CharacterStealthMode.Squat), CharacterStealthMode.Squat},
                { nameof(CharacterStealthMode.BattleMode), CharacterStealthMode.BattleMode},

                { nameof(CharacterBattleMode.Shoot), CharacterBattleMode.Shoot},
                { nameof(CharacterBattleMode.Reload), CharacterBattleMode.Reload},
            };
        }

        private void UpdateInputModules()
        {
            UpdateBaseInputModule();
            UpdateStealthInputModule();
            UpdateBattleInputModule();
        }

        protected abstract void UpdateBaseInputModule();
        protected abstract void UpdateStealthInputModule();
        protected abstract void UpdateBattleInputModule();

        [ServerRpc(Delivery = RpcDelivery.Unreliable, RequireOwnership = false)]
        protected void SetVector2ForInputActionServerRpc(string inputActionName, Vector2 inputValue, bool wasPressedThisFrame, bool isPressed)
        {
            var inputAction = _inputActionTable[inputActionName];
            var networkInputAction = (INetworkInputAction)inputAction;

            networkInputAction.SetInputValue(inputValue);
            networkInputAction.SetPressedThisFrame(wasPressedThisFrame);
            networkInputAction.SetPressed(isPressed);
        }

        [ServerRpc(Delivery = RpcDelivery.Unreliable, RequireOwnership = false)]
        protected void SetFloatForInputActionServerRpc(string inputActionName, float inputValue, bool wasPressedThisFrame, bool isPressed)
        {
            var inputAction = _inputActionTable[inputActionName];
            var networkInputAction = (INetworkInputAction)inputAction;

            networkInputAction.SetInputValue(inputValue);
            networkInputAction.SetPressedThisFrame(wasPressedThisFrame);
            networkInputAction.SetPressed(isPressed);
        }
    }
}