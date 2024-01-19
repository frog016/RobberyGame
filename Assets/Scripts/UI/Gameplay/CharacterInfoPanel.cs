using UnityEngine;

namespace UI.Gameplay
{
    public class CharacterInfoPanel : MonoBehaviour
    {
        [SerializeField] private SelectedWeaponView _weaponView;
        [SerializeField] private SelectedMagazineView _magazineView;
        [SerializeField] private PicklockView _picklockView;

        public SelectedWeaponView WeaponView => _weaponView;
        public SelectedMagazineView MagazineView => _magazineView;
        public PicklockView PicklockView => _picklockView;
    }
}