using System.Threading.Tasks;

namespace Game.Connection
{
    public interface IAuthenticationService
    {
        string PlayerId { get; }
        string PlayerName { get; set; }
        Task AuthenticateAsync();
        bool IsAuthenticated();
    }
}