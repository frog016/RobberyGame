using Game.Result;
using Structure.Netcode;
using Unity.Netcode;

namespace Presenter
{
    public partial class WinLosePanelPresenter : ServerBehaviour
    {
        private ResultObserver _resultObserver;

        protected override void OnServerNetworkSpawn()
        {
            _resultObserver = FindObjectOfType<ResultObserver>();
            _resultObserver.PlayerGameEnded += OnPlayerGameEnded;
        }

        protected override void OnServerNetworkDespawn()
        {
            _resultObserver.PlayerGameEnded -= OnPlayerGameEnded;
        }

        private void OnPlayerGameEnded(ulong clientId, bool isWin)
        {
            var rpcParam = new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new[] { clientId } } };
            OpenWinLosePanelClientRpc(isWin, rpcParam);
        }
    }
}