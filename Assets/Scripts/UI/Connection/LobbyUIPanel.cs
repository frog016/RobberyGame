using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Connection
{
    public class LobbyUIPanel : UIPanel
    {
        [SerializeField] private JoinCodeWidget _joinCodeWidget;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _leaveLobbyButton;

        public event Action StartGameButtonClicked;
        public event Action LeaveLobbyButtonClicked;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(RaiseStartGameButtonClickedEvent);
            _leaveLobbyButton.onClick.AddListener(RaiseLeaveLobbyButtonClickedEvent);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(RaiseStartGameButtonClickedEvent);
            _leaveLobbyButton.onClick.RemoveListener(RaiseLeaveLobbyButtonClickedEvent);
        }

        public void Initialize(string lobbyCode, bool isHost)
        {
            _joinCodeWidget.SetJoinCode(lobbyCode);
            _startGameButton.gameObject.SetActive(isHost);
        }

        private void RaiseStartGameButtonClickedEvent()
        {
            StartGameButtonClicked?.Invoke();
        }

        private void RaiseLeaveLobbyButtonClickedEvent()
        {
            LeaveLobbyButtonClicked?.Invoke();
        }
    }
}