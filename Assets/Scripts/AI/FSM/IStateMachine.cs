using System;
using AI.States;

namespace AI.FSM
{
    public interface IStateMachine
    {
        IState Current { get; }
        event Action<IState> Changed; 

        void SetState<T>() where T : IState;
        void SetState<T, TArg>(TArg arg) where T : IState;
        T GetState<T>() where T : IState;

        void Update();
    }
}