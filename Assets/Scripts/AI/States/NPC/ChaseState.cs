using Entity;
using UnityEngine;

namespace AI.States.NPC
{
    public class ChaseState : CharacterState, IEnterState<Character>, IUpdateState
    {
        public Character Target { get; private set; }
        public readonly float AttackDistance = 6f;

        private const float DistanceEpsilon = 1e-2f;

        public ChaseState(Character context) : base(context)
        {
        }

        public void Enter(Character target)
        {
            Ended = false;
            Target = target;
        }

        public void Update()
        {
            ChaseTarget();

            Ended = Vector2.Distance(Context.Position, Target.Position) < DistanceEpsilon;
        }

        private void ChaseTarget()
        {
            var direction = (Target.Position - Context.Position).normalized;
            Context.Movement.Move(direction);
        }
    }
}