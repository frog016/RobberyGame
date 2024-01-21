using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem.PC
{
    public class NetworkPlayerInputPC : NetworkPlayerInput
    {
        private PlayerInputMap _playerInputMap;

        protected override void InitializeInput()
        {
            _playerInputMap = new PlayerInputMap();

            CharacterBaseMode = new BaseInputModulePC(_playerInputMap);
            CharacterBattleMode = new BattleInputModulePC(_playerInputMap);
            CharacterStealthMode = new StealthInputModulePC(_playerInputMap);
        }

        protected override void UpdateBaseInputModule()
        {
            var interactAction = _playerInputMap.CharacterBaseMode.Interact;
            SetValueForInputAction<float>(nameof(CharacterBaseMode.Interact), interactAction);

            var moveAction = _playerInputMap.CharacterBaseMode.Move;
            SetValueForInputAction<Vector2>(nameof(CharacterBaseMode.Move), moveAction);

            var chargeAction = _playerInputMap.CharacterBaseMode.Charge;
            SetValueForInputAction<float>(nameof(CharacterBaseMode.Charge), chargeAction);
        }

        protected override void UpdateStealthInputModule()
        {
            var squatAction = _playerInputMap.CharacterStealthMode.Squat;
            SetValueForInputAction<float>(nameof(CharacterStealthMode.Squat), squatAction);

            var battleAction = _playerInputMap.CharacterStealthMode.BattleMode;
            SetValueForInputAction<float>(nameof(CharacterStealthMode.BattleMode), battleAction);
        }

        protected override void UpdateBattleInputModule()
        {
            var shootAction = _playerInputMap.CharacterBattleMode.Shoot;
            SetValueForInputAction<float>(nameof(CharacterBattleMode.Shoot), shootAction);

            var reloadAction = _playerInputMap.CharacterBattleMode.Reload;
            SetValueForInputAction<float>(nameof(CharacterBattleMode.Reload), reloadAction);
        }

        private void SetValueForInputAction<TReadValue>(string inputActionName, InputAction inputAction) where TReadValue : struct
        {
            var readValueType = typeof(TReadValue);
            if (readValueType == typeof(Vector2))
            {
                SetVector2ForInputActionServerRpc(
                    inputActionName,
                    inputAction.ReadValue<Vector2>(),
                    inputAction.WasPressedThisFrame(),
                    inputAction.IsPressed());
            }
            else if (readValueType == typeof(float))
            {
                SetFloatForInputActionServerRpc(
                    inputActionName,
                    inputAction.ReadValue<float>(),
                    inputAction.WasPressedThisFrame(),
                    inputAction.IsPressed());
            }
            else
            {
                throw new InvalidOperationException($"{readValueType} is not supported.");
            }
        }
    }
}