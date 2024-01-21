namespace InputSystem
{
    public interface IInputAction
    {
        T ReadValue<T>();
        bool WasPressedThisFrame();
        bool IsPressed();
    }
}