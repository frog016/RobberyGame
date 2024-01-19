using System;
using Creation.Pool;
using UnityEngine;

namespace Entity.Attack
{
    public class Magazine
    {
        public int Capacity { get; private set; }
        public int RemainingCount { get; private set; }
        public event Action<int, int> CurrentCapacityChanged;

        private readonly ProjectilePool _bulletPool;
        private readonly Transform _muzzle;

        public Magazine(ProjectilePool bulletPool, Transform muzzle, int capacity)
        {
            _bulletPool = bulletPool;
            _muzzle = muzzle;
            Capacity = capacity;

            Reload();
        }

        public Projectile GetBullet()
        {
            if (IsNotEmpty() == false)
                throw new InvalidOperationException("Impossible to take a bullet from empty magazine. Need to reload.");

            var bullet = _bulletPool.Get();
            bullet.transform.SetPositionAndRotation(_muzzle.position, _muzzle.localRotation);

            RemainingCount--;
            CurrentCapacityChanged?.Invoke(RemainingCount, Capacity);

            return bullet;
        }

        public void Reload()
        {
            RemainingCount = Capacity;
            CurrentCapacityChanged?.Invoke(RemainingCount, Capacity);
        }

        public bool IsNotEmpty()
        {
            return RemainingCount > 0;
        }
    }
}