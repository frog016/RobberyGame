using System;

namespace Entity.Inventory
{
    public class Wallet
    {
        public int Balance { get; private set; }
        public event Action<int> CurrencyChanged;

        public void Add(int value)
        {
            if (value < 0)
                throw new InvalidOperationException();

            Balance += value;
            CurrencyChanged?.Invoke(Balance);
        }

        public void Spend(int value)
        {
            if (value < 0)
                throw new InvalidOperationException();

            if (IsEnough(value) == false)
                throw new InvalidOperationException();

            Balance -= value;
            CurrencyChanged?.Invoke(Balance);
        }

        public bool IsEnough(int value)
        {
            return Balance >= value;
        }
    }
}