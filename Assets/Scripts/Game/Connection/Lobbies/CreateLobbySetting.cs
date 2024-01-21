namespace Game.Connection.Lobbies
{
    public struct CreateLobbySetting
    {
        public string MissionName;
        public int MaxConnectionLimit;

        public CreateLobbySetting(string missionName, int maxConnectionLimit)
        {
            MissionName = missionName;
            MaxConnectionLimit = maxConnectionLimit;
        }
    }
}