using System;
using System.Linq;
using Structure.Netcode;
using UnityEngine;

namespace AI.States.NPC
{
    public class PatrolWay : ServerBehaviour
    {
        [SerializeField] private PatrolPoint[] _patrolPoints;

        public PatrolWayPoint[] GetPatrolWayPoints()
        {
            return _patrolPoints
                .Select(CreatePatrolWayPoint)
                .ToArray();
        }

        private void OnDrawGizmosSelected()
        {
            const float wayPointRadius = 0.125f;
            Gizmos.color = Color.blue;

            for (var i = 0; i < _patrolPoints.Length - 1; i++)
            {
                var currentPoint = _patrolPoints[i];
                var nextPoint = _patrolPoints[i + 1];

                Gizmos.DrawSphere(currentPoint.Point.position, wayPointRadius);
                Gizmos.DrawSphere(nextPoint.Point.position, wayPointRadius);

                Gizmos.DrawLine(currentPoint.Point.position, nextPoint.Point.position);
            }
        }

        private static PatrolWayPoint CreatePatrolWayPoint(PatrolPoint point)
        {
            return new PatrolWayPoint(
                point.Point.position,
                point.StopDuration,
                point.ShouldLookAround,
                point.LookRotationAngle,
                point.ShouldInteract);
        }

        [Serializable]
        private struct PatrolPoint
        {
            [field: SerializeField] public Transform Point { get; private set; }
            [field: SerializeField] public float StopDuration { get; private set; }
            [field: SerializeField] public bool ShouldLookAround { get; private set; }
            [field: SerializeField] public float LookRotationAngle { get; private set; }
            [field: SerializeField] public bool ShouldInteract { get; private set; }
        }
    }
}