namespace InputSystem.PC
{
    public class BaseInputModulePC : BaseInputModule
    {
        private readonly PlayerInputMap _playerInputMap;

        public BaseInputModulePC(PlayerInputMap playerInputMap)
        {
            _playerInputMap = playerInputMap;
        }

        public override void Enable()
        {
            _playerInputMap.CharacterBaseMode.Enable();
        }

        public override void Disable()
        {
            _playerInputMap.CharacterBaseMode.Disable();
        }
    }
}