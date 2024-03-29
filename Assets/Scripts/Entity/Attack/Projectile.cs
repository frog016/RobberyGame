﻿using Creation.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entity.Attack
{
    public class Projectile : PoolElement<Projectile>
    {
        [field: SerializeField] public float LifeTime { get; private set; }

        [SerializeField] private float _flySpeed;
        [SerializeField] private AnimationCurve _trajectory;
        [SerializeField] private Collider2D _collider;

        private TeamId _teamId;
        private int _damage;

        public void Launch(Vector2 direction, TeamId teamId, int damage)
        {
            _teamId = teamId;
            _damage = damage;

            var destination = GetEstimationDestination(direction);
            StartCoroutine(MoveAlongTrajectoryCoroutine(destination));
        }

        public override void OnPoolGet()
        {
            gameObject.SetActive(true);
            StartCoroutine(DisposeAfterLifeTimeCoroutine());
        }

        public override void OnPoolRelease()
        {
            gameObject.SetActive(false);
        }

        protected override void OnServerTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ITarget>(out var target) && target.TeamId != _teamId && target.IsAlive())
            {
                target.ApplyDamage(_damage);
                Pool.Release(this);
            }
        }

        private Vector2 GetEstimationDestination(Vector2 direction)
        {
            const float startTime = 0f;
            const float endTime = 1f;

            var startPointHeight = Vector2.up * _trajectory.Evaluate(startTime);
            var endPointHeight = Vector2.up * _trajectory.Evaluate(endTime);

            var estimationDistance = _flySpeed * LifeTime;

            var startPoint = (Vector2)transform.position + startPointHeight;
            var endPoint = startPoint + direction * estimationDistance + endPointHeight;

            return endPoint;
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

                if (_collider.OverlapCollider(new ContactFilter2D() {layerMask = LayerMask.NameToLayer("Obstacle")}, new List<Collider2D>()) > 0)
                    yield break;

                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator DisposeAfterLifeTimeCoroutine()
        {
            yield return new WaitForSeconds(LifeTime);
            Pool.Release(this);
        }
    }
}
