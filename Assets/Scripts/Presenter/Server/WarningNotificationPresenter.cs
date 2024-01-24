using AI.States;
using Cysharp.Threading.Tasks;
using Game.State;
using Structure.Netcode;
using UnityEngine;

namespace Presenter
{
    public partial class WarningNotificationPresenter : ServerBehaviour
    {
        [SerializeField] private float _notificationActiveTime;

        protected override void OnServerNetworkSpawn()
        {
            GameStateMachine.Instance.Changed += ShowCombatNotification;
        }

        protected override void OnServerNetworkDespawn()
        {
            GameStateMachine.Instance.Changed -= ShowCombatNotification;
        }

        private void ShowCombatNotification(IState state)
        {
            if (state is not BattleGameState)
                return;

            ToggleNotification().Forget();
        }

        private async UniTaskVoid ToggleNotification()
        {
            ShowNotificationClientRpc();
            await UniTask.WaitForSeconds(_notificationActiveTime, cancellationToken: destroyCancellationToken);
            CloseNotificationClientRpc();
        }
    }
}