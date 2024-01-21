namespace InputSystem
{
    public abstract class BattleInputModule : IInputModule
    {
        public IInputAction Shoot { get; private set; } = new NetworkInputAction();
        public IInputAction Reload { get; private set; } = new NetworkInputAction();

        public abstract void Enable();
        public abstract void Disable();
    }
}