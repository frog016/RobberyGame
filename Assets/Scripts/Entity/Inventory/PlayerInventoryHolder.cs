using Structure.Netcode;

namespace Entity.Inventory
{
    public class PlayerInventoryHolder : ServerBehaviour, IInventory
    {
        public Wallet CurrencyWallet => _inventory.CurrencyWallet;
        public Wallet KeyWallet => _inventory.KeyWallet;

        private readonly PlayerInventory _inventory = new();
    }
}