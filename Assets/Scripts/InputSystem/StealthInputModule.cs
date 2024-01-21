namespace InputSystem
{
    public abstract class StealthInputModule : IInputModule
    {
        public IInputAction Squat { get; private set; } = new NetworkInputAction();
        public IInputAction BattleMode { get; private set; } = new NetworkInputAction();

        public abstract void Enable();
        public abstract void Disable();
    }
}