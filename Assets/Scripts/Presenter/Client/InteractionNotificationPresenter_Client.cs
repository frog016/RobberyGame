using UI.Notification;
using Unity.Netcode;
using UnityEngine;

namespace Presenter
{
    public partial class InteractionNotificationPresenter
    {
        [SerializeField] private InteractNotification _notification;

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