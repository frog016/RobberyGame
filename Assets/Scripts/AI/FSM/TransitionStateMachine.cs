using AI.States;
using AI.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace AI.FSM
{
    public class TransitionStateMachine : IStateMachine
    {
        public IState Current { get; private set; }
        public event Action<IState> Changed;

        private readonly Dictionary<Type, IState> _states;
        private readonly List<Transition> _anyTransitions;
        private readonly List<Transition> _transitions;

        public TransitionStateMachine(IEnumerable<IState> states,
            IEnumerable<Transition> anyTransitions,
            IEnumerable<Transition> transitions)
        {
            _states = states.ToDictionary(key => key.GetType(), value => value);
            _anyTransitions = anyTransitions.ToList();
            _transitions = transitions.ToList();

            SetDefaultState();
        }

        public void SetState<T>() where T : IState
        {
            SetState(typeof(T));
        }

        public void SetState<T, TArg>(TArg arg) where T : IState
        {
            SetState(typeof(T), arg);
        }

        public T GetState<T>() where T : IState
        {
            return (T)_states[typeof(T)];
        }

        public void Update()
        {
            SetStateByTransitions();
            Current.UpdateState();
        }

        private void SetState(Type stateType)
        {
            var newState = _states[stateType];

            Current?.ExitState();
            Current = newState;
            Current.EnterState();

            Changed?.Invoke(Current);
        }

        private void SetState<TArg>(Type stateType, TArg arg)
        {
            var newState = _states[stateType];

            Current?.ExitState();
            Current = newState;
            Current.EnterState(arg);

            Changed?.Invoke(Current);
        }

        private void SetState(Type stateType, Transition transition)
        {
            var newState = _states[stateType];

            Current?.ExitState();
            Current = newState;
            transition.SetArgumentForState(Current);

            Changed?.Invoke(Current);
        }

        private void SetStateByTransitions()
        {
            if (TryGetHappenedTransition(out var transition) == false || Current.GetType() == transition.To)
                return;

            var stateType = transition.To;
            if (stateType.IsTypeInheritsGenericInterface(typeof(IEnterState<>)))
                SetState(stateType, transition);
            else
                SetState(stateType);
        }

        private bool TryGetHappenedTransition(out Transition transition)
        {
            transition = _anyTransitions
                .FirstOrDefault(t => t.IsConditionHappened());

            if (transition != null)
                return true;

            transition = _transitions
                .Where(t => Current?.GetType() == t.From)
                .FirstOrDefault(t => t.IsConditionHappened());

            return transition != null;
        }

        private void SetDefaultState()
        {
            if (_states.ContainsKey(typeof(IdleState)) == false)
                throw new Exception(
                    $"The state machine does not have a valid list of states. By default, each should have an {nameof(IdleState)}.");

            SetState<IdleState>();
        }
    }
}
