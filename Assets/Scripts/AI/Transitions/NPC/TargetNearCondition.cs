using AI.States;
using AI.States.NPC;
using Entity;
using System;
using UnityEngine;
using Utilities;

namespace AI.Transitions.NPC
{
    public class TargetNearCondition : CharacterStateCondition
    {
        public TargetNearCondition(Character character) : base(character)
        {
        }

        public override bool IsHappened()
        {
            var chaseState = Character.StateMachine.GetState<ChaseState>();
            var target = chaseState.Target;

            return target != null
                   && Vector2.Distance(target.Position, Character.Position) < chaseState.AttackDistance;
        }

        public override void SetArgument(IState state)
        {
            var chaseState = Character.StateMachine.GetState<ChaseState>();
            var target = chaseState.Target;

            state.EnterState<Func<Vector2>>(() => (target.Position - Character.Position).normalized);
        }
    }
}