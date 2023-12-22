using Cysharp.Threading.Tasks;
using Entity;
using UnityEngine;

namespace AI.States.NPC
{
    public class PatrolState : CharacterState, IEnterState<PatrolWayPoint[]>, IUpdateState
    {
        private PatrolWayPoint[] _wayPoints;
        private TraversalOrder _pointTraversalOrder;
        private int _currentPointIndex;
        private bool _isStopped;

        private PatrolWayPoint CurrentPoint => _wayPoints[_currentPointIndex];

        private const float DistanceEpsilon = 1e-2f;

        public PatrolState(Character context) : base(context)
        {
            _pointTraversalOrder = TraversalOrder.Forward;
        }

        public void Enter(PatrolWayPoint[] points)
        {
            _wayPoints = points;
        }

        public void Update()
        {
            if (_isStopped)
                return;

            var direction = (CurrentPoint.Position - Context.Position).normalized;
            Context.Movement.Move(direction);

            if (Vector2.Distance(CurrentPoint.Position, Context.Position) < DistanceEpsilon)
                StopInWayPoint(CurrentPoint).Forget();
        }

        private async UniTaskVoid StopInWayPoint(PatrolWayPoint point)
        {
            _isStopped = true;

            if (point.ShouldLookAround)
            {
                await RotateAround(-point.LookRotationAngle, point.StopDuration / 4f);
                await RotateAround(point.LookRotationAngle, point.StopDuration / 4f);
                await RotateAround(point.LookRotationAngle, point.StopDuration / 4f);
                await RotateAround(-point.LookRotationAngle, point.StopDuration / 4f);
            }
            else if (point.ShouldInteract)
            {
                Context.StateMachine.GetState<InteractState>().SetDuration(point.StopDuration);
                Context.StateMachine.SetState<InteractState>();
            }
            else
            {
                await UniTask.WaitForSeconds(point.StopDuration, delayTiming: PlayerLoopTiming.FixedUpdate,
                    cancellationToken: Context.destroyCancellationToken);
            }

            _isStopped = false;
            NextPoint();
        }

        private async UniTask RotateAround(float angle, float duration)
        {
            var initialRotation = Context.Rotation;
            var progress = 0f;

            while (progress < 1)
            {
                progress += Time.fixedDeltaTime / duration;

                var currentAngle = Mathf.LerpAngle(0, angle, progress);
                var rotation = Quaternion.Euler(0, 0, currentAngle) * initialRotation;
                Context.Rotation = rotation.normalized;

                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, Context.destroyCancellationToken);
            }
        }

        private void NextPoint()
        {
            var nextPointIndex = _currentPointIndex + (int)_pointTraversalOrder;
            if (nextPointIndex == _wayPoints.Length || nextPointIndex == -1)
            {
                ReverseOrder();
                nextPointIndex = _currentPointIndex + (int)_pointTraversalOrder;
            }

            _currentPointIndex = nextPointIndex;
        }

        private void ReverseOrder()
        {
            var reversedOrder = -1 * (int)_pointTraversalOrder;
            _pointTraversalOrder = (TraversalOrder)reversedOrder;
        }

        public enum TraversalOrder
        {
            Forward = 1,
            Backward = -1,
        }
    }
}
