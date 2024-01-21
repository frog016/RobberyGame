namespace InputSystem.Mobile
{
    public class BaseInputModuleMobile : BaseInputModule
    {
        public readonly Joystick MoveJoystick;
        public readonly InputButton ChargeButton;
        public readonly InputButton InteractButton;

        public BaseInputModuleMobile(Joystick moveJoystick, InputButton chargeButton, InputButton interactButton)
        {
            MoveJoystick = moveJoystick;
            ChargeButton = chargeButton;
            InteractButton = interactButton;
        }

        public override void Enable()
        {
            MoveJoystick.enabled = true;
            ChargeButton.enabled = true;
            InteractButton.enabled = true;
        }

        public override void Disable()
        {
            MoveJoystick.enabled = false;
            ChargeButton.enabled = false;
            InteractButton.enabled = false;
        }
    }
}