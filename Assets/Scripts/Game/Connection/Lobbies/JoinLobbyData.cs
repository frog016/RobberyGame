namespace Game.Connection.Lobbies
{
    public struct JoinLobbyData
    {
        public string PlayerName;
        public string LobbyCode;

        public JoinLobbyData(string playerName, string lobbyCode)
        {
            PlayerName = playerName;
            LobbyCode = lobbyCode;
        }

        public JoinLobbyData(string lobbyCode)
        {
            PlayerName = "Monki";
            LobbyCode = lobbyCode;
        }
    }
}