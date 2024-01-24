using Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Connection
{
    public class LobbyUIPanel : UIPanel
    {
        [SerializeField] private JoinCodeWidget _joinCodeWidget;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _leaveLobbyButton;
        [SerializeField] private LobbyPlayerList[] _playerLists;
        [SerializeField] private LobbyStageUIPanel[] _stages;
        [SerializeField] private LobbyWeaponList _weaponList;
        [SerializeField] private LobbyMapList _mapList;

        public LobbyPlayerList[] PlayerLists => _playerLists;
        public LobbyWeaponList WeaponList => _weaponList;
        public LobbyMapList MapList => _mapList;
        public event Action StartGameButtonClicked;
        public event Action LeaveLobbyButtonClicked;

        private int _currentStageIndex;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(RaiseStartGameButtonClickedEvent);
            _leaveLobbyButton.onClick.AddListener(RaiseLeaveLobbyButtonClickedEvent);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(RaiseStartGameButtonClickedEvent);
            _leaveLobbyButton.onClick.RemoveListener(RaiseLeaveLobbyButtonClickedEvent);
        }

        public void Initialize(string lobbyCode, bool isHost)
        {
            _joinCodeWidget.SetJoinCode(lobbyCode);
            _startGameButton.gameObject.SetActive(isHost);
        }

        public void Initialize(IEnumerable<(Action backAction, Action nextAction)> stageActions)
        {
            var stageActionPairs = _stages.Zip(
                stageActions, (stage, actionPair) => (stage, actionPair.backAction, actionPair.nextAction));

            foreach (var (stage, backAction, nextAction) in stageActionPairs)
                stage.Initialize(backAction, nextAction);
        }

        public void Next()
        {
            _stages[_currentStageIndex].Close();
            _currentStageIndex = Mathf.Clamp(_currentStageIndex + 1, 0, _stages.Length - 1);
            _stages[_currentStageIndex].Open();
        }

        public void Back()
        {
            _stages[_currentStageIndex].Close();
            _currentStageIndex = Mathf.Clamp(_currentStageIndex - 1, 0, _stages.Length - 1);
            _stages[_currentStageIndex].Open();
        }

        private void RaiseStartGameButtonClickedEvent()
        {
            StartGameButtonClicked?.Invoke();
        }

        private void RaiseLeaveLobbyButtonClickedEvent()
        {
            LeaveLobbyButtonClicked?.Invoke();
        }
    }
}