using Structure.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Gameplay
{
    public class SelectedWeaponView : MonoBehaviour
    {
        [SerializeField] private Image _weaponImage;

        public void Initialize(Sprite icon)
        {
            _weaponImage.sprite = icon;
        }
    }
}