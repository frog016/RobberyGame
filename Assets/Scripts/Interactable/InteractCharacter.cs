using Entity;
using Structure.Netcode;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Interactable
{
    public class InteractCharacter : ServerBehaviour, IInteractCharacter
    {
        [field: SerializeField] public Character Character { get; private set; }

        private readonly List<IInteractable> _availableInteractable = new();

        public void Interact()
        {
            if (CanInteract() == false)
                throw new InvalidOperationException("You cannot interact with emptiness. There are no objects nearby.");

            var nearestInteractable = _availableInteractable
                .OrderBy(interactable => Vector2.Distance(Character.Position, interactable.Position))
                .First();

            nearestInteractable.Interact(Character);
        }

        public bool CanInteract()
        {
            return _availableInteractable.Count > 0;
        }

        public void AddInteractable(IInteractable interactable)
        {
            _availableInteractable.Add(interactable);
        }

        public void RemoveInteractable(IInteractable interactable)
        {
            _availableInteractable.Remove(interactable);
        }
    }
}