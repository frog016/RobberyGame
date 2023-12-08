namespace Interactable
{
    public interface IInteractCharacter
    {
        void Interact();
        bool CanInteract();

        void AddInteractable(IInteractable interactable);
        void RemoveInteractable(IInteractable interactable);
    }
}