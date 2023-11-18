namespace AI.States
{
    public interface IState
    {
        bool Ended { get; }
    }

    public interface IEnterState : IState
    {
        void Enter();
    }

    public interface IEnterState<in TArg> : IState
    {
        void Enter(TArg arg);
    }

    public interface IUpdateState : IState
    {
        void Update();
    }

    public interface IExitState : IState
    {
        void Exit();
    }
}