using UI.Notification;
using Unity.Netcode;
using UnityEngine;

namespace Presenter
{
    public partial class LevelPhaseNotificationPresenter
    {
        [SerializeField] private LevelPhaseNotification _phaseNotification;

        [ClientRpc]
        private void SetStealthPhaseClientRpc()
        {
            _phaseNotification.SetStealthPhase();
        }

        [ClientRpc]
        private void SetBattlePhaseClientRpc()
        {
            _phaseNotification.SetBattlePhase();
        }
    }
}