using System;
using Game.Connection.Lobbies;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Connection
{
    public class LobbyCreateUIPanel : UIPanel
    {
        [SerializeField] private OptionSwitcher _missionSwitcher;
        [SerializeField] private OptionSwitcher _maxPlayerCountSwitcher;
        [SerializeField] private Button _createButton;
        [SerializeField] private Button _closeButton;

        public event Action<CreateLobbySetting> CreateButtonClicked;
        public event Action CloseButtonClicked;

        private void OnEnable()
        {
            _createButton.onClick.AddListener(RaiseJoinButtonClickedEvent);
            _closeButton.onClick.AddListener(RaiseCloseButtonClickedEvent);
        }

        private void OnDisable()
        {
            _createButton.onClick.RemoveListener(RaiseJoinButtonClickedEvent);
            _closeButton.onClick.RemoveListener(RaiseCloseButtonClickedEvent);
        }

        public void Initialize(string[] missions, string[] playerCount)
        {
            _missionSwitcher.Initialize(missions);
            _maxPlayerCountSwitcher.Initialize(playerCount);
        }

        private void RaiseJoinButtonClickedEvent()
        {
            var missionName = _missionSwitcher.SelectedOption;
            var connectionLimit = int.Parse(_maxPlayerCountSwitcher.SelectedOption);
            var lobbySetting = new CreateLobbySetting(missionName, connectionLimit);

            CreateButtonClicked?.Invoke(lobbySetting);
        }

        private void RaiseCloseButtonClickedEvent()
        {
            CloseButtonClicked?.Invoke();
        }
    }
}