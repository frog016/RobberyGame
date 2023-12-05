using Structure.Netcode;
using System.Collections;
using UnityEngine;

namespace Entity.Attack
{
    public class Projectile : ServerBehaviour
    {
        [field: SerializeField] public float LifeTime { get; private set; }

        [SerializeField] private float _flySpeed;
        [SerializeField] private AnimationCurve _trajectory;

        private TeamId _teamId;
        private int _damage;

        public void Launch(Vector2 destination, TeamId teamId, int damage)
        {
            _teamId = teamId;
            _damage = damage;

            StartCoroutine(MoveAlongTrajectoryCoroutine(destination));
        }

        public void Dispose()
        {
            NetworkObject.Despawn();
        }

        protected override void OnServerTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ITarget>(out var target) && target.TeamId != _teamId && target.IsAlive())
                target.ApplyDamage(_damage);

            Dispose();
        }

        private IEnumerator MoveAlongTrajectoryCoroutine(Vector2 destination)
        {
            var startPosition = (Vector2)transform.position;
            var direction = (destination - startPosition).normalized;
            var duration = Vector2.Distance(startPosition, destination) / _flySpeed;

            var progress = 0f;
            while (progress <= 1f)
            {
                progress += Time.deltaTime / duration;
                var position = Vector2.Lerp(startPosition, destination, progress);
                var height = Vector2.up * _trajectory.Evaluate(progress);

                var trajectoryDirection = (position + height - startPosition).normalized;
                transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(direction, trajectoryDirection));
                transform.position = position + height;

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
