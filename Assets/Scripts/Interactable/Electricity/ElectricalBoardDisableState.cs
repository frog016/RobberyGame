using AI.States;

namespace Interactable.Electricity
{
    public class ElectricalBoardDisableState : ElectricalBoardState, IEnterState
    {
        public ElectricalBoardDisableState(ElectricalBoard context) : base(context)
        {
        }

        public void Enter()
        {
            Context.RaiseStateEvent(ElectricalBoarStateType.Disabled);
        }
    }
}