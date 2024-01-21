using System.Linq;
using AI.States;
using AI.States.NPC;
using Entity;
using UnityEngine;
using Utilities;

namespace AI.Transitions.NPC
{
    public class TargetFarAwayCondition : CharacterStateCondition
    {
        public TargetFarAwayCondition(Character character) : base(character)
        {
        }

        public override bool IsHappened()
        {
            var chaseState = Character.StateMachine.GetState<ChaseState>();
            var target = chaseState.Target;

            return target != null &&
                   (Vector2.Distance(target.Position, Character.Position) > chaseState.AttackDistance ||
                   HaveObstacleOnLine(Character.Position, target.Position));
        }

        public override void SetArgument(IState state)
        {
            var chaseState = Character.StateMachine.GetState<ChaseState>();
            state.EnterState<Character>(chaseState.Target);
        }

        private static bool HaveObstacleOnLine(Vector2 start, Vector2 end)
        {
            var hits = Physics2D.LinecastAll(start, end);
            return hits.Any(hit => hit.rigidbody.bodyType == RigidbodyType2D.Static && hit.collider.isTrigger == false);
        }
    }
}