using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Connection
{
    public class LobbyUIPanel : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _lobbyNameText;
        [SerializeField] private JoinCodeWidget _joinCodeWidget;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _leaveLobbyButton;
        [SerializeField] private LobbyPlayerView _playerViewPrefab;
        [SerializeField] private Transform _playerViewRoot;

        public event Action StartGameButtonClicked;
        public event Action LeaveLobbyButtonClicked;

        private readonly Dictionary<string, LobbyPlayerView> _lobbyPlayerViews = new();

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

        public void Initialize(string lobbyName, string lobbyCode, bool isHost)
        {
            _lobbyNameText.text = lobbyName;
            _joinCodeWidget.SetJoinCode(lobbyCode);

            _startGameButton.gameObject.SetActive(isHost);
        }

        public void AddPlayerView(string playerId, string playerName)
        {
            var playerView = Instantiate(_playerViewPrefab, _playerViewRoot);
            playerView.Initialize(playerName);

            _lobbyPlayerViews[playerId] = playerView;
        }

        public void RemovePlayerView(string playerId)
        {
            var playerView = _lobbyPlayerViews[playerId];
            Destroy(playerView.gameObject);

            _lobbyPlayerViews.Remove(playerId);
        }

        public void Clear()
        {
            foreach (var playerId in _lobbyPlayerViews.Keys.ToArray())
                RemovePlayerView(playerId);

            _lobbyPlayerViews.Clear();
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