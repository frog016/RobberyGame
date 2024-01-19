using Game.Connection;
using Game.Connection.Lobbies;
using UI.Connection;
using UnityEngine;

namespace Presenter
{
    public class ConnectionManagerPresenter : MonoBehaviour
    {
        [SerializeField] private ConnectionUIPanel _connectionPanel;
        [SerializeField] private RandomNamePresenter _randomNamePresenter;

        private IAuthenticationService _authenticationService;
        private ConnectionManager _connectionManager;

        public void Constructor(IAuthenticationService authenticationService, ConnectionManager connectionManager)
        {
            _authenticationService = authenticationService;
            _connectionManager = connectionManager;
        }

        private void OnEnable()
        {
            _connectionPanel.CreateButtonClicked += CreateHostLobby;
            _connectionPanel.JoinButtonClicked += JoinClientToLobby;
        }

        private void OnDisable()
        {
            _connectionPanel.CreateButtonClicked -= CreateHostLobby;
            _connectionPanel.JoinButtonClicked -= JoinClientToLobby;
        }

        private async void CreateHostLobby()
        {
            var gameJoinCode = await _connectionManager.CreateGameAsync(_connectionManager.MaxConnectionLimit);

            var createLobbyData = new CreateLobbyData(GetNameOrRandom(), gameJoinCode, _connectionManager.MaxConnectionLimit);
            await _connectionManager.CreateLobbyAsync(createLobbyData);

            _connectionPanel.Close();
        }

        private async void JoinClientToLobby(string joinCode)
        {
            var joinLobbyData = new JoinLobbyData(GetNameOrRandom(), joinCode);
            await _connectionManager.JoinLobbyAsync(joinLobbyData);

            var gameJoinCode = _connectionManager.CurrentLobby.Data[nameof(CreateLobbyData.GameJoinCode)].Value;
            await _connectionManager.JoinGameAsync(gameJoinCode);

            _connectionPanel.Close();
        }

        private string GetNameOrRandom()
        {
            if (_randomNamePresenter.IsNameEmpty())
                _randomNamePresenter.RandomizeName();

            var playerName = _randomNamePresenter.GetName();
            _authenticationService.PlayerName = playerName;

            return playerName;
        }
    }
}