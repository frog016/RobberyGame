using AI.States;
using Game.State;
using Structure.Netcode;

namespace Presenter
{
    public partial class LevelPhaseNotificationPresenter : ServerBehaviour
    {
        protected override void OnServerNetworkSpawn()
        {
            GameStateMachine.Instance.Changed += ChangePhaseNotification;
            ChangePhaseNotification(GameStateMachine.Instance.Current);
        }

        protected override void OnServerNetworkDespawn()
        {
            GameStateMachine.Instance.Changed -= ChangePhaseNotification;
        }

        private void ChangePhaseNotification(IState state)
        {
            switch (state)
            {
                case StealthGameState:
                    SetStealthPhaseClientRpc();
                    break;
                case BattleGameState:
                    SetBattlePhaseClientRpc();
                    break;
            }
        }
    }
}