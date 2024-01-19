using System;
using System.Threading.Tasks;
using Unity.Services.Lobbies.Models;

namespace Game.Connection.Lobbies
{
    public interface ILobbyConnection
    {
        Lobby CurrentLobby { get; }
        event Action<Lobby> LobbyRefreshed;
        Task<string> CreateLobbyAsync(CreateLobbyData lobbyData);
        Task JoinLobbyAsync(JoinLobbyData lobbyData);
        Task LeaveLobbyAsync();
    }
}