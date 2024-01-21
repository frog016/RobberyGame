using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _exitButton;

        public event Action StartGameButtonClicked;
        public event Action ExitButtonClicked;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(RaiseStartGameButtonClickedEvent);
            _exitButton.onClick.AddListener(RaiseExitButtonClickedEvent);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(RaiseStartGameButtonClickedEvent);
            _exitButton.onClick.RemoveListener(RaiseExitButtonClickedEvent);
        }

        private void RaiseStartGameButtonClickedEvent()
        {
            StartGameButtonClicked?.Invoke();
        }

        private void RaiseExitButtonClickedEvent()
        {
            ExitButtonClicked?.Invoke();
        }
    }
}