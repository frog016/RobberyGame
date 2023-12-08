using Entity;
using Entity.Movement;

namespace Interactable
{
    public interface IInteractable : ITransformable
    {
        void Interact(Character character);
    }
}
