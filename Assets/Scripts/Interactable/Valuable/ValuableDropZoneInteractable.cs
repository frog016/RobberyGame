using Entity;
using Entity.Inventory;
using UnityEngine;

namespace Interactable.Valuable
{
    public class ValuableDropZoneInteractable : InteractableBase
    {
        public Wallet DroppedLoot { get; } = new();

        public override void Interact(Character character) { }

        protected override void OnServerTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IInteractCharacter>(out var interact) == false || 
                interact.Character.TryGetComponent<IInventory>(out var inventory) == false)
                return;

            var lootAmount = inventory.CurrencyWallet.Balance;
            if (lootAmount == 0)
                return;

            DroppedLoot.Add(lootAmount);
            inventory.CurrencyWallet.Spend(lootAmount);
        }

        protected override void OnServerTriggerExit2D(Collider2D other) { }
    }
}