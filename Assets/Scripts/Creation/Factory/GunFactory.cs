using Config;
using Creation.Pool;
using Entity;
using Entity.Attack;
using System.Threading;

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

            var gun = new Gun(cooldown, magazine);
            gun.Initialize(config.ShootDamage, config.BulletLaunchCount, config.BulletLaunchDelay, config.ShootSpread, context.TeamId);

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
    }
}