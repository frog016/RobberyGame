using AI.States;

namespace Interactable.Electricity
{
    public abstract class ElectricalBoardState : IState
    {
        public bool Ended { get; protected set; }

        protected readonly ElectricalBoard Context;

        protected ElectricalBoardState(ElectricalBoard context)
        {
            Context = context;
        }
    }
}