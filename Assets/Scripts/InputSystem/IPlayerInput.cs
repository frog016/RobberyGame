namespace InputSystem
{
    public interface IPlayerInput
    {
        BaseInputModule CharacterBaseMode { get; }
        StealthInputModule CharacterStealthMode { get; }
        BattleInputModule CharacterBattleMode { get; }
    }
}
