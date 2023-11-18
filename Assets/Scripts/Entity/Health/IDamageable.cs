using System;

namespace Entity.Health
{
    public interface IDamageable
    {
        int Health { get; }
        int MaxHealth { get; }
        event Action<int> HealthChanged;

        void ApplyDamage(int amount);
    }
}
