using Config;
using Game.Connection;
using Structure.Scene;
using System;
using UI.Connection;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Presenter
{
    public class LobbyPresenter : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private LobbyUIPanel _lobbyPanel;
        [SerializeField] private LobbyConnectionUIPanel _connectionPanel;
        [SerializeField] private GunConfig[] _initialGunConfigs;

        [Header("Data")]
        [SerializeField] private string _gameplayViewName;
        [SerializeField] private string _map1ViewName;
        [SerializeField] private string _map2ViewName;

        private ISceneLoader _sceneLoader;
        private ConnectionManager _connectionManager;

        private string _selectedMap;

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
            _lobbyPanel.WeaponList.WeaponSelected += OnWeaponSelected;
            _lobbyPanel.MapList.MapSelected += OnMapSelected;
            _connectionManager.LobbyRefreshed += OnLobbyRefreshed;

            _lobbyPanel.Initialize(new (Action, Action)[]
            {
                (EmptyAction, _lobbyPanel.Next),
                (_lobbyPanel.Back, _lobbyPanel.Next),
                (_lobbyPanel.Back, _lobbyPanel.Next),
                (_lobbyPanel.Back, EmptyAction)
            });

            _lobbyPanel.WeaponList.Initialize(_initialGunConfigs);
            _lobbyPanel.MapList.Initialize(new[] {
                (SceneNames.GameplayScene, _gameplayViewName),
                (SceneNames.GameplayMap1, _map1ViewName),
                (SceneNames.GameplayMap2, _map2ViewName) });

            OnWeaponSelected(_initialGunConfigs[0]);
            OnMapSelected(SceneNames.GameplayScene);
        }

        private void OnMapSelected(string mapName)
        {
            _selectedMap = mapName;
        }

        public override void OnNetworkDespawn()
        {
            if (IsServer)
                _lobbyPanel.StartGameButtonClicked -= StartGame;

            _lobbyPanel.LeaveLobbyButtonClicked -= LeaveLobby;
            _lobbyPanel.WeaponList.WeaponSelected -= OnWeaponSelected;
            _connectionManager.LobbyRefreshed -= OnLobbyRefreshed;
        }

        private void OnWeaponSelected(GunConfig gunConfig)
        {
            SetSelectedGunConfigServerRpc(NetworkManager.LocalClientId, gunConfig.GetInstanceID());
        }

        private async void StartGame()
        {
            await _sceneLoader.LoadAsync(_selectedMap);
        }

        private async void LeaveLobby()
        {
            await _connectionManager.LeaveLobbyAsync();
            NetworkManager.Shutdown();

            _lobbyPanel.Close();
            foreach (var playerList in _lobbyPanel.PlayerLists)
                playerList.Clear();

            _connectionPanel.Open();
        }

        private void OnLobbyRefreshed(Lobby lobby)
        {
            _lobbyPanel.Initialize(lobby.LobbyCode, IsServer);

            foreach (var playerList in _lobbyPanel.PlayerLists)
            {
                playerList.Clear();
                foreach (var player in lobby.Players)
                    playerList.AddPlayerView(player.Id, player.Data["PlayerName"].Value);
            }

            if (_lobbyPanel.IsActive == false)
                _lobbyPanel.Open();
        }

        private void EmptyAction() { }

        [ServerRpc]
        private void SetSelectedGunConfigServerRpc(ulong clientId, int instanceId)
        {
            var gunConfig = (GunConfig)Resources.InstanceIDToObject(instanceId);
            StartGameConfig.SelectedStartGun[clientId] = gunConfig;
        }
    }
}