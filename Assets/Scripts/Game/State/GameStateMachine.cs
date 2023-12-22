using AI.FSM;
using AI.States;
using System;

namespace Game.State
{
    public class GameStateMachine : IStateMachine
    {
        public static GameStateMachine Instance { get; private set; }

        public IState Current => _stateMachineImplementation.Current;

        public event Action<IState> Changed
        {
            add => _stateMachineImplementation.Changed += value;
            remove => _stateMachineImplementation.Changed -= value;
        }

        private readonly IStateMachine _stateMachineImplementation;

        public GameStateMachine(IStateMachine stateMachineImplementation)
        {
            Instance = this;
            _stateMachineImplementation = stateMachineImplementation;
        }

        public void SetState<T>() where T : IState
        {
            _stateMachineImplementation.SetState<T>();
        }

        public void SetState<T, TArg>(TArg arg) where T : IState
        {
            _stateMachineImplementation.SetState<T, TArg>(arg);
        }

        public T GetState<T>() where T : IState
        {
            return _stateMachineImplementation.GetState<T>();
        }

        public void Update()
        {
            _stateMachineImplementation.Update();
        }
    }
}
