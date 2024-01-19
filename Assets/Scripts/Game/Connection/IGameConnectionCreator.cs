using System.Threading.Tasks;

namespace Game.Connection
{
    public interface IGameConnectionCreator
    {
        Task<string> CreateGameAsync(int maxConnectionLimit);
        Task JoinGameAsync(string joinCode);
    }
}