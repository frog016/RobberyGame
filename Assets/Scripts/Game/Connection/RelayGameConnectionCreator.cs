using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Relay;

namespace Game.Connection
{
    public class RelayGameConnectionCreator : IGameConnectionCreator
    {
        private readonly IRelayService _relayService;
        private readonly NetworkManager _networkManager;
        private readonly UnityTransport _transport;

        public RelayGameConnectionCreator(IRelayService relayService, NetworkManager networkManager, UnityTransport transport)
        {
            _relayService = relayService;
            _networkManager = networkManager;
            _transport = transport;
        }

        public async Task<string> CreateGameAsync(int maxConnectionLimit)
        {
            var allocation = await _relayService.CreateAllocationAsync(maxConnectionLimit);
            _transport.SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData);

            _networkManager.StartHost();
            return await _relayService.GetJoinCodeAsync(allocation.AllocationId); ;
        }

        public async Task JoinGameAsync(string joinCode)
        {
            var joinAllocation = await _relayService.JoinAllocationAsync(joinCode);
            _transport.SetClientRelayData(
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData);

            _networkManager.StartClient();
        }
    }
}