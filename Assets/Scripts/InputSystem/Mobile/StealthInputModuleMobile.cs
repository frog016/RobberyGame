namespace InputSystem.Mobile
{
    public class StealthInputModuleMobile : StealthInputModule
    {
        public readonly InputButton SquatButton;
        public readonly InputButton BattleModeButton;

        public StealthInputModuleMobile(InputButton squatButton, InputButton battleModeButton)
        {
            SquatButton = squatButton;
            BattleModeButton = battleModeButton;
        }

        public override void Enable()
        {
            SquatButton.enabled = true;
            BattleModeButton.enabled = true;
        }

        public override void Disable()
        {
            SquatButton.enabled = false;
            BattleModeButton.enabled = false;
        }
    }
}