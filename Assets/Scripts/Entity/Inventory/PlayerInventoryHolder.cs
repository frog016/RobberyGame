using Structure.Netcode;

namespace Entity.Inventory
{
    public class PlayerInventoryHolder : ServerBehaviour, IInventory
    {
        public Wallet CurrencyWallet => _inventory.CurrencyWallet;

        private readonly PlayerInventory _inventory = new();
    }
}