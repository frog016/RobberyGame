using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Presenter
{
    public class LocalPlayerUIPresenter : NetworkBehaviour
    {
        [SerializeField] private GameObject _ui;
        [SerializeField] private EventSystem _eventSystem;

        public override void OnNetworkSpawn()
        {
            if (IsLocalPlayer || OwnerClientId == NetworkManager.LocalClientId)
                return;

            if (_ui != null)
                _ui.SetActive(false);

            if (_eventSystem != null)
                _eventSystem.gameObject.SetActive(false);
        }
    }
}