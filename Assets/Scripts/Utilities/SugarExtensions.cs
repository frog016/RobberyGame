using AI.States;

namespace Utilities
{
    public static class SugarExtensions
    {
        public static void EnterState(this IState state)
        {
            if (state is IEnterState enterState)
                enterState.Enter();
        }

        public static void EnterState<T>(this IState state, T arg)
        {
            if (state is IEnterState<T> enterState)
                enterState.Enter(arg);
        }

        public static void UpdateState(this IState state)
        {
            if (state is IUpdateState updateState)
                updateState.Update();
        }

        public static void ExitState(this IState state)
        {
            if (state is IExitState exitState)
                exitState.Exit();
        }
    }
}