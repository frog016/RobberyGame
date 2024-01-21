using Cinemachine;
using Unity.Netcode;
using UnityEngine;

namespace Presenter
{
    public class LocalPlayerCamera : NetworkBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        public override void OnNetworkSpawn()
        {
            if (IsLocalPlayer || OwnerClientId != NetworkManager.LocalClientId)
                return;

            if (_camera != null)
                _camera.gameObject.SetActive(false);

            if (_virtualCamera != null)
                _virtualCamera.gameObject.SetActive(false);
        }
    }
}