using Entity;
using Entity.Inventory;

namespace Interactable.Valuable
{
    public class ValuableInteractable : InteractableBase
    {
        public override void Interact(Character character)
        {
            if (character.TryGetComponent<IInventory>(out var inventory))
            {
                inventory.CurrencyWallet.Add(1);
                DestroyInteractable(character);
            }
        }

        private void DestroyInteractable(Character character)
        {
            character.GetComponent<IInteractCharacter>().RemoveInteractable(this);
            NetworkObject.Despawn();
        }
    }
}
