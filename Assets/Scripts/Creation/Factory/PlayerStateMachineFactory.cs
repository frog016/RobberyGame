using System.Collections.Generic;
using AI.FSM;
using AI.States;
using AI.Transitions;
using Config;
using Entity;
using Entity.Attack;
using InputSystem;
using UnityEngine;

namespace Creation.Factory
{
    public class PlayerStateMachineFactory : ICharacterStateMachineFactory
    {
        public TeamId CharacterTeamId => TeamId.Player;

        private readonly IPlayerInput _playerInput;

        public PlayerStateMachineFactory(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public IStateMachine CreateStateMachine(Character context, CharacterConfig config, object extraArgument = null)
        {
            var states = CreateStates(context, config);
            var anyTransitions = CreateAnyTransitions(context, _playerInput);
            var transitions = CreateTransitions(context, _playerInput);

            return new TransitionStateMachine(states, anyTransitions, transitions);
        }

        private IEnumerable<IState> CreateStates(Character character, CharacterConfig config)
        {
            yield return new IdleState();
            yield return new WalkState(character);
            yield return new ChargeState(character, config.ChargeSpeed, config.ChargeDuration);
            yield return new SquatState(character, config.SquatSpeed);
            yield return new BattleModeState(character, _playerInput);
            yield return new ReloadState(character, config.ReloadDuration);
            yield return new ShootState(character);
            yield return new InteractState(character, config.InteractDuration);
        }

        private static IEnumerable<Transition> CreateAnyTransitions(Character character, IPlayerInput input)
        {
            var launchModeCondition = new HaveBattleModeInputCondition(character, input);
            yield return new Transition(null, typeof(BattleModeState), launchModeCondition);

            var reloadInputCondition = new HaveReloadInputCondition(character, input);
            yield return new Transition(null, typeof(ReloadState), reloadInputCondition);
        }

        private static IEnumerable<Transition> CreateTransitions(Character character, IPlayerInput input)
        {
            var launchEndedCondition = new StateEndedCondition<BattleModeState>(character);
            yield return new Transition(typeof(BattleModeState), typeof(IdleState), launchEndedCondition);

            var reloadInputCondition = new StateEndedCondition<ReloadState>(character);
            yield return new Transition(typeof(ReloadState), typeof(IdleState), reloadInputCondition);

            var chargeInputCondition = new HaveChargeInputCondition(character, input);
            yield return new Transition(typeof(IdleState), typeof(ChargeState), chargeInputCondition);
            yield return new Transition(typeof(WalkState), typeof(ChargeState), chargeInputCondition);

            var chargeEndedCondition = new StateEndedCondition<ChargeState>(character);
            yield return new Transition(typeof(ChargeState), typeof(IdleState), chargeEndedCondition);

            var squatInputCondition = new HaveSquatInputCondition(character, input);
            yield return new Transition(typeof(IdleState), typeof(SquatState), squatInputCondition);
            yield return new Transition(typeof(WalkState), typeof(SquatState), squatInputCondition);
            yield return new Transition(typeof(SquatState), typeof(IdleState), squatInputCondition);

            var haveInputCondition = new HaveMovementInputCondition(character, input);
            yield return new Transition(typeof(IdleState), typeof(WalkState), haveInputCondition);
            yield return new Transition(typeof(WalkState), typeof(IdleState), haveInputCondition, true);

            var interactInputCondition = new HaveInteractInputCondition(character, input);
            yield return new Transition(typeof(IdleState), typeof(InteractState), interactInputCondition);

            var interactionEndedCondition = new StateEndedCondition<InteractState>(character);
            yield return new Transition(typeof(InteractState), typeof(IdleState), interactionEndedCondition);

            var shootInputCondition = new HaveShootInputCondition(character, input);
            yield return new Transition(typeof(IdleState), typeof(ShootState), shootInputCondition);
            yield return new Transition(typeof(SquatState), typeof(ShootState), shootInputCondition);
            yield return new Transition(typeof(WalkState), typeof(ShootState), shootInputCondition);
            yield return new Transition(typeof(ShootState), typeof(IdleState), shootInputCondition, true);
        }
    }
}