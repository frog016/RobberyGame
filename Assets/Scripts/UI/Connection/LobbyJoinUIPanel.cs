using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Connection
{
    public class LobbyJoinUIPanel : UIPanel
    {
        [SerializeField] private Button _joinButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_InputField _codeInputField;

        public event Action<string> JoinButtonClicked;
        public event Action CloseButtonClicked;

        private void OnEnable()
        {
            _joinButton.onClick.AddListener(RaiseJoinButtonClickedEvent);
            _closeButton.onClick.AddListener(RaiseCloseButtonClickedEvent);
        }

        private void OnDisable()
        {
            _joinButton.onClick.RemoveListener(RaiseJoinButtonClickedEvent);
            _closeButton.onClick.RemoveListener(RaiseCloseButtonClickedEvent);
        }

        private void RaiseJoinButtonClickedEvent()
        {
            var code = _codeInputField.text;
            JoinButtonClicked?.Invoke(code);
        }

        private void RaiseCloseButtonClickedEvent()
        {
            CloseButtonClicked?.Invoke();
        }
    }
}