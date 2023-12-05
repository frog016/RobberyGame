using AI.FSM;
using AI.States;
using AI.Transitions;
using Entity.Movement;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Entity
{
    public class TestPlayer : NetworkBehaviour
    {
        [SerializeField] private Character _playerCharacter;
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _speed;
        [SerializeField] private float _chargeSpeed;
        [SerializeField] private float _chargeDuration;
        [SerializeField] private float _squatSpeed;
        [SerializeField] private float _reloadDuration;

        private PlayerInput _playerInput;

        public override void OnNetworkSpawn()
        {
            if (IsLocalPlayer == false)
                return;

            _playerInput = new PlayerInput();
            _playerInput.Enable();

            Initialize();
        }

        private void Initialize()
        {
            var movement = new CharacterMovement(_playerCharacter, _speed);
            var stateMachine = CreateStateMachine();

            //_playerCharacter.Initialize(movement, stateMachine, _maxHealth);
        }

        private IStateMachine CreateStateMachine()
        {
            var states = CreateStates(_playerCharacter, _chargeSpeed, _chargeDuration, _squatSpeed, _reloadDuration);
            var anyTransitions = CreateAnyTransitions(_playerCharacter, _playerInput);
            var transitions = CreateTransitions(_playerCharacter, _playerInput);

            return new TransitionStateMachine(states, anyTransitions, transitions);
        }

        private static IEnumerable<IState> CreateStates(Character character, float chargeSpeed, float chargeDuration, float squatSpeed, float reloadDuration)
        {
            yield return new IdleState();
            yield return new WalkState(character);
            yield return new ChargeState(character, chargeSpeed, chargeDuration);
            yield return new SquatState(character, squatSpeed);
            yield return new BattleModeState(character);
            yield return new ReloadState(character, reloadDuration);
            yield return new InteractState(character);
        }

        private static IEnumerable<Transition> CreateAnyTransitions(Character character, PlayerInput input)
        {
            var launchModeCondition = new HaveBattleModeInputCondition(character, input);
            yield return new Transition(null, typeof(BattleModeState), launchModeCondition);

            var reloadInputCondition = new HaveReloadInputCondition(character, input);
            yield return new Transition(null, typeof(ReloadState), reloadInputCondition);
        }

        private static IEnumerable<Transition> CreateTransitions(Character character, PlayerInput input)
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
        }
    }
}