using Structure.Netcode;
using UI.Gameplay;
using UnityEngine;

namespace Presenter.Client
{
    public class PausePanelPresenter : ClientBehaviour
    {
        [SerializeField] private PausePanel _pausePanel;

        protected override void OnClientNetworkSpawn()
        {
            _pausePanel.ReturnButtonClicked += OnReturnClicked;
            _pausePanel.ExitButtonClicked += OnExitClicked;
        }

        protected override void OnClientNetworkDespawn()
        {
            _pausePanel.ReturnButtonClicked -= OnReturnClicked;
            _pausePanel.ExitButtonClicked -= OnExitClicked;
        }

        private void OnReturnClicked()
        {
            _pausePanel.Close();
        }

        private void OnExitClicked()
        {
            NetworkManager.Shutdown();
            Application.Quit();
        }
    }
}
