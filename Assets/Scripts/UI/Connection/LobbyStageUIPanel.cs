using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Connection
{
    public class LobbyStageUIPanel : UIPanel
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _nextButton;

        private Action _backAction;
        private Action _nextAction;

        public void Initialize(Action backAction, Action nextAction)
        {
            _backAction = backAction;
            _nextAction = nextAction;

            _backButton.onClick.AddListener(_backAction.Invoke);
            _nextButton.onClick.AddListener(_nextAction.Invoke);
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveListener(_backAction.Invoke);
            _nextButton.onClick.RemoveListener(_nextAction.Invoke);
        }
    }
}