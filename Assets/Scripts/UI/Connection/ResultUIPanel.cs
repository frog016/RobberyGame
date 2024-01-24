using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Connection
{
    public class ResultUIPanel : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private Button _exitButton;

        public event Action ExitButtonClicked;

        public void Initialize(string result)
        {
            _resultText.text = result;

            _exitButton.onClick.AddListener(RaiseExitClicked);
        }

        private void OnDestroy()
        {
            _exitButton.onClick.RemoveListener(RaiseExitClicked);
        }

        private void RaiseExitClicked()
        {
            ExitButtonClicked?.Invoke();
        }
    }
}