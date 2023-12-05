using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Entity.Attack
{
    public class Gun
    {
        public readonly Cooldown Cooldown;
        
        private readonly Magazine _magazine;

        private int _damage;
        private int _bulletLaunchCount;
        private float _bulletLaunchDelay;
        private float _spread;
        private TeamId _teamId;

        public Gun(Cooldown cooldown, Magazine magazine)
        {
            Cooldown = cooldown;
            _magazine = magazine;
        }

        public void Initialize(int damage, int bulletLaunchCount, float bulletLaunchDelay, float spread, TeamId teamId)
        {
            _damage = damage;
            _bulletLaunchCount = bulletLaunchCount;
            _bulletLaunchDelay = bulletLaunchDelay;
            _spread = spread;
            _teamId = teamId;
        }

        public void Shoot(Vector2 direction)
        {
            ShootAsync(direction).Forget();
        }

        private async UniTaskVoid ShootAsync(Vector2 direction)
        {
            Cooldown.Restart();

            for (var i = 0; i < _bulletLaunchCount; i++)
            {
                var bullet = _magazine.GetBullet();
                var shootDirection = direction + GetSpreadDirection();

                bullet.Launch(shootDirection, _teamId, _damage);

                await UniTask.WaitForSeconds(_bulletLaunchDelay);
            }
        }

        private Vector2 GetSpreadDirection()
        {
            return new Vector2(
                Random.Range(-_spread, _spread),
                Random.Range(-_spread, _spread));
        }
    }
}
