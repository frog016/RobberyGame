namespace InputSystem.PC
{
    public class StealthInputModulePC : StealthInputModule
    {
        private readonly PlayerInputMap _playerInputMap;

        public StealthInputModulePC(PlayerInputMap playerInputMap)
        {
            _playerInputMap = playerInputMap;
        }

        public override void Enable()
        {
            _playerInputMap.CharacterStealthMode.Enable();
        }

        public override void Disable()
        {
            _playerInputMap.CharacterStealthMode.Disable();
        }
    }
}
