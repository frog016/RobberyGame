﻿namespace Entity.Inventory
{
    public interface IInventory
    {
        Wallet CurrencyWallet { get; }
        Wallet KeyWallet { get; }
    }
}