using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Connection
{
    public class ConnectionUIPanel : UIPanel
    {
        [SerializeField] private Button _createButton;
        [SerializeField] private Button _joinButton;
        [SerializeField] private TMP_InputField _joinCodeInputField;

        public event Action CreateButtonClicked;
        public event Action<string> JoinButtonClicked;

        private void OnEnable()
        {
            _createButton.onClick.AddListener(RaiseCreateButtonClickedEvent);
            _joinButton.onClick.AddListener(RaiseJoinButtonClickedEvent);
        }

        private void OnDisable()
        {
            _createButton.onClick.RemoveListener(RaiseCreateButtonClickedEvent);
            _joinButton.onClick.RemoveListener(RaiseJoinButtonClickedEvent);
        }

        private void RaiseCreateButtonClickedEvent()
        {
            CreateButtonClicked?.Invoke();
        }

        private void RaiseJoinButtonClickedEvent()
        {
            var joinCode = _joinCodeInputField.text;
            JoinButtonClicked?.Invoke(joinCode);
        }
    }
}