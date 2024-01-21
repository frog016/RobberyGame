using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OptionSwitcher : MonoBehaviour
    {
        [SerializeField] private Button _switchLeftButton;
        [SerializeField] private Button _switchRightButton;
        [SerializeField] private TextMeshProUGUI _selectedOptionText;
        public string SelectedOption => _options[_selectedOptionIndex];

        private string[] _options;
        private int _selectedOptionIndex;

        private void OnEnable()
        {
            _switchLeftButton.onClick.AddListener(SwitchToLeft);
            _switchRightButton.onClick.AddListener(SwitchToRight);
        }

        private void OnDisable()
        {
            _switchLeftButton.onClick.RemoveListener(SwitchToLeft);
            _switchRightButton.onClick.RemoveListener(SwitchToRight);
        }

        public void Initialize(string[] options)
        {
            _options = options;
            _selectedOptionIndex = 0;

            DisplaySelectedOption();
        }

        private void SwitchToLeft()
        {
            SwitchOption(-1);
            DisplaySelectedOption();
        }

        private void SwitchToRight()
        {
            SwitchOption(1);
            DisplaySelectedOption();
        }

        private void SwitchOption(int direction)
        {
            _selectedOptionIndex = Mathf.Clamp(_selectedOptionIndex + direction, 0, _options.Length - 1);
        }

        private void DisplaySelectedOption()
        {
            _selectedOptionText.text = SelectedOption;
        }
    }
}