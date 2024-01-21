namespace InputSystem.Mobile
{
    public class BattleInputModuleMobile : BattleInputModule
    {
        public readonly Joystick ShootJoystick;
        public readonly InputButton ReloadButton;

        public BattleInputModuleMobile(Joystick shootJoystick, InputButton reloadButton)
        {
            ShootJoystick = shootJoystick;
            ReloadButton = reloadButton;
        }

        public override void Enable()
        {
            ShootJoystick.enabled = true;
            ReloadButton.enabled = true;
        }

        public override void Disable()
        {
            ShootJoystick.enabled = false;
            ReloadButton.enabled = false;
        }
    }
}