using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Game.Connection.Lobbies
{
    public class UnityLobbyServiceConnection : ILobbyConnection
    {
        public Lobby CurrentLobby { get; private set; }
        public event Action<Lobby> LobbyRefreshed;

        private readonly ILobbyService _lobbyService;
        private readonly IAuthenticationService _authenticationService;

        private CancellationTokenSource _heartbeatLobbySource;
        private CancellationTokenSource _refreshLobbySource;

        private const int HeartbeatInterval = 25;
        private const int LobbyRefreshRate = 4;

        public UnityLobbyServiceConnection(ILobbyService lobbyService, IAuthenticationService authenticationService)
        {
            _lobbyService = lobbyService;
            _authenticationService = authenticationService;
        }

        public async Task<string> CreateLobbyAsync(CreateLobbyData lobbyData)
        {
            var createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = true,
                Player = CreateLobbyPlayer(lobbyData.PlayerName),
                Data = new Dictionary<string, DataObject>
                {
                    { nameof(lobbyData.GameJoinCode), new DataObject(DataObject.VisibilityOptions.Member, lobbyData.GameJoinCode) },
                    { nameof(lobbyData.CreateLobbySetting.MissionName), new DataObject(DataObject.VisibilityOptions.Member, lobbyData.CreateLobbySetting.MissionName) }
                },
            };

            CurrentLobby = await _lobbyService.CreateLobbyAsync("Monki", lobbyData.CreateLobbySetting.MaxConnectionLimit, createLobbyOptions);

            HeartbeatLobbyAsync();
            RefreshLobbyAsync();

            return CurrentLobby.LobbyCode;
        }


        public async Task JoinLobbyAsync(JoinLobbyData lobbyData)
        {
            var joinLobbyOptions = new JoinLobbyByCodeOptions
            {
                Player = CreateLobbyPlayer(lobbyData.PlayerName)
            };

            CurrentLobby = await Unity.Services.Lobbies.Lobbies.Instance.JoinLobbyByCodeAsync(lobbyData.LobbyCode, joinLobbyOptions);

            RefreshLobbyAsync();
        }

        public async Task LeaveLobbyAsync()
        {
            _heartbeatLobbySource?.Cancel();
            _refreshLobbySource?.Cancel();

            if (CurrentLobby == null)
                return;

            try
            {
                var playerId = _authenticationService.PlayerId;
                if (CurrentLobby.HostId == playerId)
                    await _lobbyService.DeleteLobbyAsync(CurrentLobby.Id);
                else
                    await _lobbyService.RemovePlayerAsync(CurrentLobby.Id, playerId);

                CurrentLobby = null;
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }

        private async void HeartbeatLobbyAsync()
        {
            _heartbeatLobbySource = new CancellationTokenSource();
            while (_heartbeatLobbySource.IsCancellationRequested == false && CurrentLobby != null)
            {
                await _lobbyService.SendHeartbeatPingAsync(CurrentLobby.Id);
                await Task.Delay(HeartbeatInterval * 1000);
            }
        }

        private async void RefreshLobbyAsync()
        {
            _refreshLobbySource = new CancellationTokenSource();
            while (_refreshLobbySource.IsCancellationRequested == false && CurrentLobby != null)
            {
                CurrentLobby = await _lobbyService.GetLobbyAsync(CurrentLobby.Id);
                LobbyRefreshed?.Invoke(CurrentLobby);

                await Task.Delay(LobbyRefreshRate * 1000);
            }
        }

        private Player CreateLobbyPlayer(string playerName)
        {
            var playerId = _authenticationService.PlayerId;
            var playerData = new Dictionary<string, PlayerDataObject>
            {
                {"PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName)}
            };

            return new Player(playerId, data: playerData);
        }
    }
}