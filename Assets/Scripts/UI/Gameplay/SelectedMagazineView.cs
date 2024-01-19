using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Gameplay
{
    public class SelectedMagazineView : MonoBehaviour
    {
        [SerializeField] private Image _bulletImage;
        [SerializeField] private TextMeshProUGUI _magazineCapacityText;

        public void Initialize(Sprite icon, int currentCapacity, int maxCapacity)
        {
            _bulletImage.sprite = icon;
            SetMagazineCapacity(currentCapacity, maxCapacity);
        }

        public void SetMagazineCapacity(int currentCapacity, int maxCapacity)
        {
            _magazineCapacityText.text = $"{currentCapacity}|{maxCapacity}";
        }
    }
}