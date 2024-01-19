using Animation;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Navigation
{
    public class DoorPhysicBody : MonoBehaviour
    {
        [SerializeField] private NavMeshObstacle _navMeshObstacle;
        [SerializeField] private DoorAnimator _doorAnimator;

        public const float DelayBeforeOpen = 0;
        public const float DelayBeforeClose = 2;

        private void Awake()
        {
            _navMeshObstacle.carveOnlyStationary = false;
        }

        public void Open(Vector3 interactPosition, float delay = DelayBeforeOpen)
        {
            var direction = GetOpenDirection(interactPosition);
            _doorAnimator.AnimateOpen(direction, delay, () =>
            {
                _navMeshObstacle.enabled = true;
                _navMeshObstacle.carving = true;
            });
        }

        public void Close(float delay = DelayBeforeClose)
        {
            _navMeshObstacle.carving = false;
            _navMeshObstacle.enabled = false;

            _doorAnimator.AnimateClose(delay);
        }

        private int GetOpenDirection(Vector2 interactPosition)
        {
            var directionVector = (interactPosition - (Vector2)transform.position).normalized;
            var direction = Vector2.Dot(transform.right, directionVector);

            return -(int)Mathf.Sign(direction);
        }
    }
}