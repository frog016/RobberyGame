namespace InputSystem
{
    public interface INetworkInputAction
    {
        void SetInputValue(object value);
        void SetPressedThisFrame(bool value);
        void SetPressed(bool value);
    }
}