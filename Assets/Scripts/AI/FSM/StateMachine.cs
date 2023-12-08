using System;
using System.Collections.Generic;
using System.Linq;
using AI.States;
using Utilities;

namespace AI.FSM
{
    public class StateMachine : IStateMachine
    {
        public IState Current { get; private set; }
        public event Action<IState> Changed;

        private readonly Dictionary<Type, IState> _states;

        public StateMachine(IEnumerable<IState> states)
        {
            _states = states.ToDictionary(key => key.GetType(), value => value);
        }

        public void SetState<T>() where T : IState
        {
            Current?.ExitState();
            Current = GetState<T>();
            Current.EnterState();

            Changed?.Invoke(Current);
        }

        public void SetState<T, TArg>(TArg arg) where T : IState
        {
            Current?.ExitState();
            Current = GetState<T>();
            Current.EnterState(arg);

            Changed?.Invoke(Current);
        }

        public T GetState<T>() where T : IState
        {
            return (T)_states[typeof(T)];
        }

        public void Update()
        {
            Current.UpdateState();
        }
    }
}