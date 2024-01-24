namespace Game.Connection.Lobbies
{
    public struct JoinLobbyData
    {
        public string PlayerName;
        public string LobbyCode;

        public JoinLobbyData(string playerName, string lobbyCode)
        {
            PlayerName = "Monki " + playerName;
            LobbyCode = lobbyCode;
        }
    }
}