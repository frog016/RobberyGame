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

        private static PatrolWayPoint CreatePatrolWayPoint(PatrolPoint point)
        {
            return new PatrolWayPoint(
                point.Point.position,
                point.StopDuration,
                point.ShouldLookAround,
                point.LookRotationAngle);
        }

        [Serializable]
        private struct PatrolPoint
        {
            [field: SerializeField] public Transform Point { get; private set; }
            [field: SerializeField] public float StopDuration { get; private set; }
            [field: SerializeField] public bool ShouldLookAround { get; private set; }
            [field: SerializeField] public float LookRotationAngle { get; private set; }
        }
    }
}