using Creation.Factory;
using Entity.Attack;
using UnityEngine;
using UnityEngine.Pool;

namespace Creation.Pool
{
    public class ProjectilePool : IObjectPool<Projectile>
    {
        public int CountInactive => _internalPool.CountInactive;

        private readonly IFactory _factory;
        private readonly Projectile _prefab;
        private readonly ObjectPool<Projectile> _internalPool;

        public ProjectilePool(IFactory factory, Projectile prefab)
        {
            _factory = factory;
            _prefab = prefab;

            _internalPool = CreatePool();
        }

        public Projectile Get()
        {
            return _internalPool.Get();
        }

        public PooledObject<Projectile> Get(out Projectile v)
        {
            return _internalPool.Get(out v);
        }

        public void Release(Projectile element)
        {
            _internalPool.Release(element);
        }

        public void Clear()
        {
            _internalPool.Clear();
        }

        private ObjectPool<Projectile> CreatePool()
        {
            var objectPool = new ObjectPool<Projectile>(
                CreateProjectile,
                OnGetProjectile,
                OnReleaseProjectile);

            return objectPool;
        }

        private Projectile CreateProjectile()
        {
            var projectile = _factory.CreateForComponent(_prefab);
            projectile.Initialize(this);
            projectile.NetworkObject.Spawn(true);

            return projectile;
        }

        private static void OnGetProjectile(Projectile projectile)
        {
            projectile.OnPoolGet();
        }

        private static void OnReleaseProjectile(Projectile projectile)
        {
            projectile.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            projectile.OnPoolRelease();
        }
    }
}