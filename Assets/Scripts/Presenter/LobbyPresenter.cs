using Game.Connection;
using Structure.Scene;
using UI.Connection;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Presenter
{
    public class LobbyPresenter : NetworkBehaviour
    {
        [SerializeField] private LobbyUIPanel _lobbyPanel;
        [SerializeField] private ConnectionUIPanel _connectionPanel;

        private ISceneLoader _sceneLoader;
        private ConnectionManager _connectionManager;

        public void Constructor(ISceneLoader sceneLoader, ConnectionManager connectionManager)
        {
            _sceneLoader = sceneLoader;
            _connectionManager = connectionManager;
        }

        public override void OnNetworkSpawn()
        {
            if (IsServer)
                _lobbyPanel.StartGameButtonClicked += StartGame;

            _lobbyPanel.LeaveLobbyButtonClicked += LeaveLobby;
            _connectionManager.LobbyRefreshed += OnLobbyRefreshed;
        }

        public override void OnNetworkDespawn()
        {
            if (IsServer)
                _lobbyPanel.StartGameButtonClicked -= StartGame;

            _lobbyPanel.LeaveLobbyButtonClicked -= LeaveLobby;
            _connectionManager.LobbyRefreshed -= OnLobbyRefreshed;
        }

        private async void StartGame()
        {
            await _sceneLoader.LoadAsync(SceneNames.GameplayScene);
        }

        private async void LeaveLobby()
        {
            await _connectionManager.LeaveLobbyAsync();
            NetworkManager.Shutdown();

            _lobbyPanel.Clear();
            _lobbyPanel.Close();

            _connectionPanel.Open();
        }

        private void OnLobbyRefreshed(Lobby lobby)
        {
            _lobbyPanel.Clear();
            _lobbyPanel.Initialize(lobby.Name, lobby.LobbyCode, IsServer);

            foreach (var player in lobby.Players)
                _lobbyPanel.AddPlayerView(player.Id, player.Data["PlayerName"].Value);

            if (_lobbyPanel.IsActive == false)
                _lobbyPanel.Open();
        }
    }
}