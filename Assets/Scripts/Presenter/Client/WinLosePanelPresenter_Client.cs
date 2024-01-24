using Cysharp.Threading.Tasks;
using Structure.Scene;
using Structure.Service;
using UI.Connection;
using Unity.Netcode;
using UnityEngine;

namespace Presenter
{
    public partial class WinLosePanelPresenter
    {
        [SerializeField] private WinLoseUIPanel _winLosePanel;

        private ISceneLoader _sceneLoader;

        public override void OnNetworkSpawn()
        {
            if (IsClient)
            {
                _sceneLoader = ServiceLocator.Instance.Get<ISceneLoader>();
                _winLosePanel.ExitClicked += OnExitClicked;
            }

            base.OnNetworkSpawn();
        }

        private void OnExitClicked()
        {
            NetworkManager.Shutdown();
            _sceneLoader.LoadAsync(SceneNames.MainMenuScene).Forget();
        }

        [ClientRpc]
        private void OpenWinLosePanelClientRpc(bool isWin, ClientRpcParams rpcParam = default)
        {
            _winLosePanel.Open();
            _winLosePanel.Initialize(isWin);
        }
    }
}