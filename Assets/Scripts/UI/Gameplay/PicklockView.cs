using TMPro;
using UnityEngine;

namespace UI.Gameplay
{
    public class PicklockView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentValueText;

        public void SetCount(int count)
        {
            _currentValueText.text = count.ToString();
        }
    }
}