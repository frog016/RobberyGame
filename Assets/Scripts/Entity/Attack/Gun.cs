using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entity.Attack
{
    public class Gun
    {
        public readonly Cooldown Cooldown;
        public readonly Magazine Magazine;

        private int _damage;
        private int _bulletLaunchCount;
        private float _bulletLaunchDelay;
        private float _spread;
        private TeamId _teamId;

        public Gun(Cooldown cooldown, Magazine magazine)
        {
            Cooldown = cooldown;
            Magazine = magazine;
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

        public void Reload()
        {
            Magazine.Reload();
        }

        public bool IsMagazineEmpty()
        {
            return Magazine.IsNotEmpty() == false;
        }

        private async UniTaskVoid ShootAsync(Vector2 direction)
        {
            Cooldown.Restart();

            for (var i = 0; i < _bulletLaunchCount; i++)
            {
                if (Magazine.IsNotEmpty() == false)
                {
                    Debug.Log("Reload!");
                    break;
                }

                var bullet = Magazine.GetBullet();
                var shootDirection = (direction + GetSpreadDirection()).normalized;

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
