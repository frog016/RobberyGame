using Cysharp.Threading.Tasks;
using Structure.Netcode;
using Structure.Scene;
using Structure.Service;
using UI.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.Client
{
    public class PausePanelPresenter : ClientBehaviour
    {
        [SerializeField] private PausePanel _pausePanel;
        [SerializeField] private Button _pauseButton;

        private ISceneLoader _sceneLoader;

        protected override void OnClientNetworkSpawn()
        {
            _sceneLoader = ServiceLocator.Instance.Get<ISceneLoader>();

            _pauseButton.onClick.AddListener(OpenPausePanel);

            _pausePanel.ReturnButtonClicked += OnReturnClicked;
            _pausePanel.ExitButtonClicked += OnExitClicked;
        }

        protected override void OnClientNetworkDespawn()
        {
            _pauseButton.onClick.RemoveListener(OpenPausePanel);

            _pausePanel.ReturnButtonClicked -= OnReturnClicked;
            _pausePanel.ExitButtonClicked -= OnExitClicked;
        }

        private void OpenPausePanel()
        {
            _pausePanel.Open();
        }

        private void OnReturnClicked()
        {
            _pausePanel.Close();
        }

        private void OnExitClicked()
        {
            NetworkManager.Shutdown();
            _sceneLoader.LoadAsync(SceneNames.MainMenuScene).Forget();
        }
    }
}
