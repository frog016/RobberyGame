using Entity;
using Interactable;
using Structure.Netcode;
using UnityEngine;

namespace Presenter
{
    public partial class InteractionNotificationPresenter : ServerBehaviour
    {
        [SerializeField] private Character _character;

        private IInteractCharacter _interactCharacter;
        private bool _canInteract;

        protected override void OnServerNetworkSpawn()
        {
            _interactCharacter = _character.GetComponent<IInteractCharacter>();
        }

        protected override void OnServerFixedUpdate()
        {
            UpdateNotification();
        }

        private void UpdateNotification()
        {
            var canInteract = _interactCharacter.CanInteract();
            if (canInteract == _canInteract)
                return;

            _canInteract = canInteract;
            if (_canInteract)
                ShowNotificationClientRpc();
            else
                CloseNotificationClientRpc();
        }
    }
}