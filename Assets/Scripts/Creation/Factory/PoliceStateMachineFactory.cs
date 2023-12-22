using AI.FSM;
using AI.States;
using AI.States.NPC;
using AI.Transitions;
using AI.Transitions.NPC;
using Config;
using Entity;
using Entity.Attack;
using System;
using System.Collections.Generic;

namespace Creation.Factory
{
    public class PoliceStateMachineFactory : ICharacterStateMachineFactory
    {
        public TeamId CharacterTeamId => TeamId.Police;

        public IStateMachine CreateStateMachine(Character context, CharacterConfig config, object extraArgument = null)
        {
            if (extraArgument is not PatrolWayPoint[] wayPoints)
                throw new ArgumentException($"Argument {extraArgument} is not {nameof(PatrolWayPoint)} array.");

            var states = CreateStates(context, config);
            var anyTransitions = CreateAnyTransitions(context);
            var transitions = CreateTransitions(context, wayPoints);

            return new TransitionStateMachine(states, anyTransitions, transitions);
        }

        private static IEnumerable<IState> CreateStates(Character character, CharacterConfig config)
        {
            yield return new IdleState();
            yield return new ReloadState(character, config.ReloadDuration);
            yield return new ShootState(character);
            yield return new InteractState(character, config.InteractDuration);
            yield return new PatrolState(character);
        }

        private static IEnumerable<Transition> CreateAnyTransitions(Character character)
        {
            var reloadCondition = new NeedToAutoReloadCondition(character);
            yield return new Transition(null, typeof(ReloadState), reloadCondition);
        }

        private static IEnumerable<Transition> CreateTransitions(Character character, PatrolWayPoint[] wayPoints)
        {
            var reloadEndedCondition = new StateEndedCondition<ReloadState>(character);
            yield return new Transition(typeof(ReloadState), typeof(IdleState), reloadEndedCondition);

            var interactionEndedCondition = new StateEndedCondition<InteractState>(character);
            yield return new Transition(typeof(InteractState), typeof(IdleState), interactionEndedCondition);

            var patrolCondition = new ReadyToPatrolCondition(character, wayPoints);
            yield return new Transition(typeof(IdleState), typeof(PatrolState), patrolCondition);

            var interactEndedCondition = new StateEndedCondition<InteractState>(character);
            yield return new Transition(typeof(InteractState), typeof(IdleState), interactEndedCondition);
        }
    }
}