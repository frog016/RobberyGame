using Entity;
using Structure.Netcode;
using UnityEngine;

namespace Interactable
{
    public abstract class InteractableBase : ServerBehaviour, IInteractable
    {
        public Vector2 Position { get => transform.position; set => transform.position = value; }
        public Vector2 Rotation { get; set; } = Vector2.right;

        public abstract void Interact(Character character);

        protected override void OnServerTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IInteractCharacter>(out var interact) == false)
                return;

            interact.AddInteractable(this);
        }

        protected override void OnServerTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<IInteractCharacter>(out var interact) == false)
                return;

            interact.RemoveInteractable(this);
        }
    }
}