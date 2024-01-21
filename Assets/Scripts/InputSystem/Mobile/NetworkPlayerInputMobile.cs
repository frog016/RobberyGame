using UnityEngine;

namespace InputSystem.Mobile
{
    public class NetworkPlayerInputMobile : NetworkPlayerInput
    {
        [Header("Base Input")]
        [SerializeField] private Joystick _moveJoystick;
        [SerializeField] private InputButton _chargeButton;
        [SerializeField] private InputButton _interactButton;

        [Header("Stealth Input")]
        [SerializeField] private InputButton _squatButton;
        [SerializeField] private InputButton _battleModeButton;

        [Header("Battle Input")]
        [SerializeField] private Joystick _shootJoystick;
        [SerializeField] private InputButton _reloadButton;

        protected override void InitializeInput()
        {
            CharacterBaseMode = new BaseInputModuleMobile(_moveJoystick, _chargeButton, _interactButton);
            CharacterStealthMode = new StealthInputModuleMobile(_squatButton, _battleModeButton);
            CharacterBattleMode = new BattleInputModuleMobile(_shootJoystick, _reloadButton);
        }

        protected override void UpdateBaseInputModule()
        {
            var baseModuleMobile = (BaseInputModuleMobile)CharacterBaseMode;

            SetValueFromButtonForInputAction(nameof(baseModuleMobile.Interact), baseModuleMobile.InteractButton);
            SetValueFromJoystickForInputAction(nameof(baseModuleMobile.Move), baseModuleMobile.MoveJoystick);
            SetValueFromButtonForInputAction(nameof(baseModuleMobile.Charge), baseModuleMobile.ChargeButton);
        }

        protected override void UpdateStealthInputModule()
        {
            var stealthModuleMobile = (StealthInputModuleMobile)CharacterStealthMode;

            SetValueFromButtonForInputAction(nameof(stealthModuleMobile.Squat), stealthModuleMobile.SquatButton);
            SetValueFromButtonForInputAction(nameof(stealthModuleMobile.BattleMode), stealthModuleMobile.BattleModeButton);
        }

        protected override void UpdateBattleInputModule()
        {
            var battleModuleMobile = (BattleInputModuleMobile)CharacterBattleMode;

            SetValueFromJoystickForInputAction(nameof(battleModuleMobile.Shoot), battleModuleMobile.ShootJoystick);
            SetValueFromButtonForInputAction(nameof(battleModuleMobile.Reload), battleModuleMobile.ReloadButton);
        }

        private void SetValueFromButtonForInputAction(string inputActionName, InputButton button)
        {
            var buttonValue = button.IsPressed ? 1f : -1f;
            SetFloatForInputActionServerRpc(inputActionName, buttonValue, button.IsPressedThisFrame, button.IsPressed);
        }

        private void SetValueFromJoystickForInputAction(string inputActionName, Joystick joystick)
        {
            var joystickValue = new Vector2(joystick.Horizontal, joystick.Vertical);
            var isPressed = joystick.Horizontal != 0 || joystick.Vertical != 0;

            SetVector2ForInputActionServerRpc(inputActionName, joystickValue, isPressed, isPressed);
        }
    }
}
