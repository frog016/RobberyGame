namespace Entity.Inventory
{
    public class PlayerInventory : IInventory
    {
        public Wallet CurrencyWallet { get; } = new();
    }
}
