using UnityEngine;

namespace AI.States.NPC
{
    public readonly struct PatrolWayPoint
    {
        public readonly Vector2 Position;
        public readonly float StopDuration;
        public readonly bool ShouldLookAround;
        public readonly float LookRotationAngle;
        public readonly bool ShouldInteract;

        public PatrolWayPoint(Vector2 position, float stopDuration, bool shouldLookAround, float lookRotationAngle, bool shouldInteract)
        {
            Position = position;
            StopDuration = stopDuration;
            ShouldLookAround = shouldLookAround;
            LookRotationAngle = lookRotationAngle;
            ShouldInteract = shouldInteract;
        }
    }
}