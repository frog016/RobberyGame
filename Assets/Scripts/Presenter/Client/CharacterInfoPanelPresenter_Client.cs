using UI.Gameplay;
using Unity.Netcode;
using UnityEngine;

namespace Presenter
{
    public partial class CharacterInfoPanelPresenter
    {
        [SerializeField] private CharacterInfoPanel _characterInfoPanel;

        [ClientRpc]
        private void InitializeWeaponViewClientRpc()
        {
            _characterInfoPanel.WeaponView.Initialize(null);
        }

        [ClientRpc]
        private void InitializeMagazineViewClientRpc(int remainingCount, int capacity)
        {
            _characterInfoPanel.MagazineView.Initialize(null, remainingCount, capacity);
        }

        [ClientRpc]
        private void SetMagazineCapacityClientRpc(int remainingCount, int capacity)
        {
            _characterInfoPanel.MagazineView.SetMagazineCapacity(remainingCount, capacity);
        }

        [ClientRpc]
        private void SetPickLockCountClientRpc(int picklockCount)
        {
            _characterInfoPanel.PicklockView.SetCount(picklockCount);
        }
    }
}