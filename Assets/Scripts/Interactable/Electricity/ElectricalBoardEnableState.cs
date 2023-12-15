using AI.States;

namespace Interactable.Electricity
{
    public class ElectricalBoardEnableState : ElectricalBoardState, IEnterState
    {
        public ElectricalBoardEnableState(ElectricalBoard context) : base(context)
        {
        }

        public void Enter()
        {
            Context.RaiseStateEvent(ElectricalBoarStateType.Enabled);
        }
    }
}