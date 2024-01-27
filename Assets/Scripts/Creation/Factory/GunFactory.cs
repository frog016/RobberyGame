using Config;
using Creation.Pool;
using Entity;
using Entity.Attack;
using System.Threading;
using Unity.Netcode;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Creation.Factory
{
    public class GunFactory
    {
        private readonly ProjectilePoolProvider _poolProvider;

        public GunFactory(ProjectilePoolProvider poolProvider)
        {
            _poolProvider = poolProvider;
        }

        public Gun Create(GunConfig config, Character context)
        {
            var cooldown = CreateCooldown(config, context.destroyCancellationToken);
            var magazine = CreateMagazine(config, context);

            var gun = new Gun(cooldown, magazine, context.destroyCancellationToken);
            gun.Initialize(config.ShootDamage, config.BulletLaunchCount, config.BulletLaunchDelay, config.ShootSpread, context.TeamId);

            CreateGunView(config, context);

            return gun;
        }

        private static Cooldown CreateCooldown(GunConfig config, CancellationToken cancellationToken)
        {
            return new Cooldown(config.ShootCooldown, cancellationToken);
        }

        private Magazine CreateMagazine(GunConfig config, Character context)
        {
            var pool = _poolProvider.GetPool(config.ProjectilePrefab);
            return new Magazine(pool, context.Muzzle, config.MagazineCapacity);
        }

        private static void CreateGunView(GunConfig config, Character context)
        {
            var gunView = Object.Instantiate(config.ViewPrefab, context.transform);
            context.Muzzle.localPosition = gunView.MuzzlePositionLocal;

            gunView.Initialize(context, context.TeamId == TeamId.Player ? context.GetComponentInChildren<Camera>() : null);

            var gunNetworkObject = gunView.GetComponent<NetworkObject>();
            gunNetworkObject.SpawnWithOwnership(context.OwnerClientId, true);
            gunNetworkObject.TrySetParent(context.transform);
        }
    }
}