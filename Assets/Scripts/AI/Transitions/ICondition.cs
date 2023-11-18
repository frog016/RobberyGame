using AI.States;

namespace AI.Transitions
{
    public interface ICondition
    {
        bool IsHappened();
        void SetArgument(IState state);
    }
}