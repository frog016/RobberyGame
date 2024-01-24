namespace Game.Connection.Lobbies
{
    public struct CreateLobbyData
    {
        public string PlayerName;
        public string GameJoinCode;
        public CreateLobbySetting CreateLobbySetting;

        public CreateLobbyData(string playerName, string gameJoinCode, CreateLobbySetting createLobbySetting)
        {
            PlayerName = "Host " + playerName;
            GameJoinCode = gameJoinCode;
            CreateLobbySetting = createLobbySetting;
        }
    }
}