using System;
using Structure.Netcode;
using Unity.Netcode;
using UnityEngine;

namespace Entity.Health
{
    public class NetworkDamageable : ServerBehaviour, IDamageable
    {
        public int Health { get => _netHealth.Value; protected set => _netHealth.Value = value; }
        public int MaxHealth { get; protected set; }
        public event Action<int> HealthChanged;

        private readonly NetworkVariable<int> _netHealth = new();

        public void ApplyDamage(int amount)
        {
            Health = Mathf.Max(0, Health - amount);
            HealthChanged?.Invoke(Health);

            if (Health > 0)
                return;

            DestroyDamageable();
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        protected virtual void DestroyDamageable() { }
    }
}