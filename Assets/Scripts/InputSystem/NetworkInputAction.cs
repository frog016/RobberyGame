namespace InputSystem
{
    public class NetworkInputAction : IInputAction, INetworkInputAction
    {
        private object _value;
        private bool _wasPressedThisFrame;
        private bool _isPressed;

        public T ReadValue<T>()
        {
            return _value is null ? default : (T)_value;
        }

        public bool WasPressedThisFrame()
        {
            return _wasPressedThisFrame;
        }

        public bool IsPressed()
        {
            return _isPressed;
        }

        public void SetInputValue(object value)
        {
            _value = value;
        }

        public void SetPressedThisFrame(bool value)
        {
            _wasPressedThisFrame = value;
        }

        public void SetPressed(bool value)
        {
            _isPressed = value;
        }
    }
}