using Entity;
using Entity.Inventory;
using Structure.Netcode;

namespace Presenter
{
    public partial class CharacterInfoPanelPresenter : ServerBehaviour
    {
        private Character _character;

        public void Initialize(Character character)
        {
            _character = character;

            InitializeWeaponViewClientRpc();

            var magazine = _character.AttackBehaviour.Gun.Magazine;
            InitializeMagazineViewClientRpc(magazine.RemainingCount, magazine.Capacity);
            magazine.CurrentCapacityChanged += SetMagazineCapacityClientRpc;

            var inventory = _character.GetComponent<IInventory>();
            SetPickLockCountClientRpc(inventory.KeyWallet.Balance);
            inventory.KeyWallet.CurrencyChanged += SetPickLockCountClientRpc;
        }

        protected override void OnServerNetworkDespawn()
        {
            var magazine = _character.AttackBehaviour.Gun.Magazine;
            magazine.CurrentCapacityChanged -= SetMagazineCapacityClientRpc;

            var inventory = _character.GetComponent<IInventory>();
            inventory.KeyWallet.CurrencyChanged -= SetPickLockCountClientRpc;
        }
    }
}