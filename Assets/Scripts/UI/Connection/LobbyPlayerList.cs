using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Connection
{
    public class LobbyPlayerList : MonoBehaviour
    {
        [SerializeField] private LobbyPlayerView _playerViewPrefab;
        [SerializeField] private Transform _playerViewRoot;

        private readonly Dictionary<string, LobbyPlayerView> _lobbyPlayerViews = new();

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
    }
}