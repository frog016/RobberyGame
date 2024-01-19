using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Presenter
{
    public class PresenterRoot : NetworkBehaviour
    {
        [SerializeField] private GameObject _ui;
        [SerializeField] private EventSystem _eventSystem;

        public void Initialize()
        {
            var presenters = GetComponentsInChildren<IUIPresenter>();
            foreach (var presenter in presenters)
            {
                presenter.Initialize();
            }
        }

        public override void OnNetworkSpawn()
        {
            if (IsLocalPlayer == false)
                return;

            _ui.SetActive(false);
            _eventSystem.gameObject.SetActive(false);
        }
    }
}
