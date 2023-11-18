using System;
using AI.States;

namespace AI.Transitions
{
    public class Transition
    {
        public readonly Type From;
        public readonly Type To;

        protected readonly ICondition Condition;
        protected readonly bool Reverse;

        public Transition(Type from, Type to, ICondition condition, bool reverse = false)
        {
            From = from;
            To = to;
            Condition = condition;
            Reverse = reverse;
        }

        public bool IsConditionHappened() => Condition.IsHappened() ^ Reverse;

        public void SetArgumentForState(IState state) => Condition.SetArgument(state);
    }
}