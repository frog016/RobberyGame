namespace InputSystem.PC
{
    public class BattleInputModulePC : BattleInputModule
    {
        private readonly PlayerInputMap _playerInputMap;

        public BattleInputModulePC(PlayerInputMap playerInputMap)
        {
            _playerInputMap = playerInputMap;
        }

        public override void Enable()
        {
            _playerInputMap.CharacterBattleMode.Enable();
        }

        public override void Disable()
        {
            _playerInputMap.CharacterBattleMode.Disable();
        }
    }
}