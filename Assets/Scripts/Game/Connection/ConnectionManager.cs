using System;
using System.Threading.Tasks;
using Game.Connection.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Game.Connection
{
    public class ConnectionManager : MonoBehaviour, IGameConnectionCreator, ILobbyConnection
    {
        [field: SerializeField] public int MaxConnectionLimit { get; private set; }

        public Lobby CurrentLobby => _lobbyConnection.CurrentLobby;
        public event Action<Lobby> LobbyRefreshed
        {
            add => _lobbyConnection.LobbyRefreshed += value;
            remove => _lobbyConnection.LobbyRefreshed -= value;
        }

        private IGameConnectionCreator _connectionCreator;
        private ILobbyConnection _lobbyConnection;

        public void Constructor(IGameConnectionCreator connectionCreator, ILobbyConnection lobbyConnection)
        {
            _connectionCreator = connectionCreator;
            _lobbyConnection = lobbyConnection;
        }

        public Task<string> CreateGameAsync(int maxConnectionLimit)
        {
            return _connectionCreator.CreateGameAsync(maxConnectionLimit);
        }

        public Task JoinGameAsync(string joinCode)
        {
            return _connectionCreator.JoinGameAsync(joinCode);
        }

        public Task<string> CreateLobbyAsync(CreateLobbyData lobbyData)
        {
            return _lobbyConnection.CreateLobbyAsync(lobbyData);
        }

        public Task JoinLobbyAsync(JoinLobbyData lobbyData)
        {
            return _lobbyConnection.JoinLobbyAsync(lobbyData);
        }

        public Task LeaveLobbyAsync()
        {
            return _lobbyConnection.LeaveLobbyAsync();
        }
    }
}