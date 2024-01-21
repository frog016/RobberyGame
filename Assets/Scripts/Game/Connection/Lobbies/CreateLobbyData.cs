namespace Game.Connection.Lobbies
{
    public struct CreateLobbyData
    {
        public string PlayerName;
        public string GameJoinCode;
        public CreateLobbySetting CreateLobbySetting;

        public CreateLobbyData(string playerName, string gameJoinCode, CreateLobbySetting createLobbySetting)
        {
            PlayerName = playerName;
            GameJoinCode = gameJoinCode;
            CreateLobbySetting = createLobbySetting;
        }

        public CreateLobbyData(string gameJoinCode, CreateLobbySetting createLobbySetting)
        {
            PlayerName = "Host Monki";
            GameJoinCode = gameJoinCode;
            CreateLobbySetting = createLobbySetting;
        }
    }
}