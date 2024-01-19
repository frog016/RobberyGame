namespace Game.Connection.Lobbies
{
    public struct CreateLobbyData
    {
        public string PlayerName;
        public string LobbyName;
        public string GameJoinCode;
        public int MaxConnectionLimit;

        private const string LobbyTestName = "Monki Lobby";

        public CreateLobbyData(string playerName, string lobbyName, string gameJoinCode, int maxConnectionLimit)
        {
            PlayerName = playerName;
            LobbyName = lobbyName;
            GameJoinCode = gameJoinCode;
            MaxConnectionLimit = maxConnectionLimit;
        }

        public CreateLobbyData(string playerName, string gameJoinCode, int maxConnectionLimit)
        {
            PlayerName = playerName;
            LobbyName = LobbyTestName;
            GameJoinCode = gameJoinCode;
            MaxConnectionLimit = maxConnectionLimit;
        }
    }
}