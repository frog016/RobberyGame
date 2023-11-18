using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartNetworkUI : MonoBehaviour
    {
        [SerializeField] private Button _startClientButton;
        [SerializeField] private Button _startHostButton;
        [SerializeField] private Button _startServerButton;
        [SerializeField] private NetworkManager _networkManager;

        private void OnEnable()
        {
            _startClientButton.onClick.AddListener(StartClient);
            _startHostButton.onClick.AddListener(StartHost);
            _startServerButton.onClick.AddListener(StartServer);
        }

        private void OnDisable()
        {
            _startClientButton.onClick.RemoveListener(StartClient);
            _startHostButton.onClick.RemoveListener(StartHost);
            _startServerButton.onClick.RemoveListener(StartServer);
        }

        private void StartClient()
        {
            _networkManager.StartClient();
            gameObject.SetActive(false);
        }

        private void StartHost()
        {
            _networkManager.StartHost();
            gameObject.SetActive(false);
        }

        private void StartServer()
        {
            _networkManager.StartServer();
            gameObject.SetActive(false);
        }
    }
}