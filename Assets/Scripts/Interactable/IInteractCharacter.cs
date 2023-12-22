using Entity;

namespace Interactable
{
    public interface IInteractCharacter
    {
        Character Character { get; }
        void Interact();
        void EndInteract();
        bool CanInteract();

        void AddInteractable(IInteractable interactable);
        void RemoveInteractable(IInteractable interactable);
    }
}