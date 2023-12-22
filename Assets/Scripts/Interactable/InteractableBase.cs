using Entity;
using Structure.Netcode;
using UnityEngine;

namespace Interactable
{
    public abstract class InteractableBase : ServerBehaviour, IInteractable
    {
        public Vector2 Position { get => transform.position; set => transform.position = value; }
        public Vector2 Rotation { get; set; }

        protected virtual void Awake()
        {
            Rotation = transform.rotation * Vector3.right;
        }

        public abstract void Interact(Character character);
        public virtual void EndInteract(Character character) { }

        protected override void OnServerTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IInteractCharacter>(out var interact) == false)
                return;

            interact.AddInteractable(this);
            OnCharacterTriggerEnter(interact.Character);
        }

        protected override void OnServerTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<IInteractCharacter>(out var interact) == false)
                return;

            interact.RemoveInteractable(this);
            OnCharacterTriggerExit(interact.Character);
        }

        protected virtual void OnCharacterTriggerEnter(Character character) { }
        protected virtual void OnCharacterTriggerExit(Character character) { }
    }
}