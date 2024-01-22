namespace InputSystem
{
    public abstract class BaseInputModule : IInputModule
    {
        public IInputAction Interact { get; private set; } = new NetworkInputAction();
        public IInputAction Move { get; private set; } = new NetworkInputAction();
        public IInputAction Charge { get; private set; } = new NetworkInputAction();

        public abstract void Enable();
        public abstract void Disable();
    }
}