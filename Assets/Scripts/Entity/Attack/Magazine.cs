using UnityEngine;

namespace Entity.Attack
{
    public class Magazine
    {
        private readonly Projectile _bulletPrefab;
        private readonly Transform _muzzle;

        public Magazine(Projectile bulletPrefab, Transform muzzle)
        {
            _bulletPrefab = bulletPrefab;
            _muzzle = muzzle;
        }

        public Projectile GetBullet()
        {
            var instance = Object.Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation);
            instance.NetworkObject.Spawn(true);

            return instance;
        }
    }
}