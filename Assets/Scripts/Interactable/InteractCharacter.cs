using System;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Structure.Netcode;
using UnityEngine;

namespace Interactable
{
    public class InteractCharacter : ServerBehaviour, IInteractCharacter
    {
        [SerializeField] private Character _character;
        
        private readonly List<IInteractable> _availableInteractable = new();

        public void Interact()
        {
            if (CanInteract() == false)
                throw new InvalidOperationException("You cannot interact with emptiness. There are no objects nearby.");

            var nearestInteractable = _availableInteractable
                .OrderBy(interactable => Vector2.Distance(_character.Position, interactable.Position))
                .First();

            nearestInteractable.Interact(_character);
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