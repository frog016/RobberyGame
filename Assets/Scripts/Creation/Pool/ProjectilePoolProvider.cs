using System;
using System.Collections.Generic;
using Creation.Factory;
using Entity.Attack;

namespace Creation.Pool
{
    public class ProjectilePoolProvider : IDisposable
    {
        private readonly IFactory _factory;
        private readonly Dictionary<int, ProjectilePool> _dictionary = new();

        public ProjectilePoolProvider(IFactory factory)
        {
            _factory = factory;
        }

        public ProjectilePool GetPool(Projectile prefab)
        {
            var prefabId = prefab.GetInstanceID();
            if (_dictionary.TryGetValue(prefabId, out var pool))
                return pool;

            var createdPool = new ProjectilePool(_factory, prefab);
            _dictionary.Add(prefabId, createdPool);

            return createdPool;
        }

        public void Dispose()
        {
            _dictionary.Clear();
        }
    }
}