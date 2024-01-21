using UI.Notification;
using Unity.Netcode;
using UnityEngine;

namespace Presenter
{
    public partial class WarningNotificationPresenter
    {
        [SerializeField] private WarningNotification _notification;

        [ClientRpc]
        private void ShowNotificationClientRpc()
        {
            _notification.Open();
        }

        [ClientRpc]
        private void CloseNotificationClientRpc()
        {
            _notification.Close();
        }
    }
}