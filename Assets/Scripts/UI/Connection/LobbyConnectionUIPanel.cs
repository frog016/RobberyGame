using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Connection
{
    public class LobbyConnectionUIPanel : UIPanel
    {
        [SerializeField] private Button _createButton;
        [SerializeField] private Button _joinButton;
        [SerializeField] private Button _closeButton;

        public event Action CreateButtonClicked;
        public event Action JoinButtonClicked;
        public event Action CloseButtonClicked;

        private void OnEnable()
        {
            _createButton.onClick.AddListener(RaiseCreateButtonClickedEvent);
            _joinButton.onClick.AddListener(RaiseJoinButtonClickedEvent);
            _closeButton.onClick.AddListener(RaiseCloseButtonClickedEvent);
        }

        private void OnDisable()
        {
            _createButton.onClick.RemoveListener(RaiseCreateButtonClickedEvent);
            _joinButton.onClick.RemoveListener(RaiseJoinButtonClickedEvent);
            _closeButton.onClick.RemoveListener(RaiseCloseButtonClickedEvent);
        }

        private void RaiseCreateButtonClickedEvent()
        {
            CreateButtonClicked?.Invoke();
        }

        private void RaiseJoinButtonClickedEvent()
        {
            JoinButtonClicked?.Invoke();
        }

        private void RaiseCloseButtonClickedEvent()
        {
            CloseButtonClicked?.Invoke();
        }
    }
}