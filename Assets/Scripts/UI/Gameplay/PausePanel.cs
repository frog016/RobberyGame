using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Gameplay
{
    public class PausePanel : UIPanel
    {
        [SerializeField] private Button _returnButton;
        [SerializeField] private Button _exitButton;

        public event Action ReturnButtonClicked;
        public event Action ExitButtonClicked;

        private void OnEnable()
        {
            _returnButton.onClick.AddListener(RaiseReturnButtonEvent);
            _exitButton.onClick.AddListener(RaiseExitButtonEvent);
        }

        private void OnDisable()
        {
            _returnButton.onClick.AddListener(RaiseReturnButtonEvent);
            _exitButton.onClick.AddListener(RaiseExitButtonEvent);
        }

        private void RaiseReturnButtonEvent()
        {
            ReturnButtonClicked?.Invoke();
        }

        private void RaiseExitButtonEvent()
        {
            ExitButtonClicked?.Invoke();
        }
    }
}
