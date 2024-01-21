using Cysharp.Threading.Tasks;
using Game.Connection;
using Game.Connection.Lobbies;
using Structure.Scene;
using System.Linq;
using UI.Connection;
using UnityEngine;

namespace Presenter
{
    public class ConnectionManagerPresenter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LobbyConnectionUIPanel _connectionPanel;
        [SerializeField] private LobbyCreateUIPanel _createPanel;
        [SerializeField] private LobbyJoinUIPanel _joinPanel;

        [Header("Settings")]
        [SerializeField] private string[] _missions;
        [SerializeField, Min(1)] private int _minPlayerCount;

        private ISceneLoader _sceneLoader;
        private ConnectionManager _connectionManager;

        public void Constructor(ISceneLoader sceneLoader, ConnectionManager connectionManager)
        {
            _sceneLoader = sceneLoader;
            _connectionManager = connectionManager;
        }

        private void OnEnable()
        {
            _connectionPanel.CreateButtonClicked += OpenCreateLobbyPanel;
            _connectionPanel.JoinButtonClicked += OpenJoinLobbyPanel;
            _connectionPanel.CloseButtonClicked += ExitToMenu;

            _createPanel.CreateButtonClicked += CreateLobby;
            _createPanel.CloseButtonClicked += CloseCreatePanel;

            _joinPanel.JoinButtonClicked += JoinLobby;
            _joinPanel.CloseButtonClicked += CloseJoinPanel;
        }

        private void Start()
        {
            var playerCount = Enumerable
                .Range(_minPlayerCount, _connectionManager.MaxConnectionLimit - _minPlayerCount + 1)
                .Select(i => i.ToString())
                .ToArray();

            _createPanel.Initialize(_missions, playerCount);
        }

        private void OnDisable()
        {
            _connectionPanel.CreateButtonClicked -= OpenCreateLobbyPanel;
            _connectionPanel.JoinButtonClicked -= OpenJoinLobbyPanel;
            _connectionPanel.CloseButtonClicked -= ExitToMenu;

            _createPanel.CreateButtonClicked -= CreateLobby;
            _createPanel.CloseButtonClicked -= CloseCreatePanel;

            _joinPanel.JoinButtonClicked -= JoinLobby;
            _joinPanel.CloseButtonClicked -= CloseJoinPanel;
        }

        private async void CreateLobby(CreateLobbySetting createLobbySetting)
        {
            var gameJoinCode = await _connectionManager.CreateGameAsync(createLobbySetting.MaxConnectionLimit);

            var createLobbyData = new CreateLobbyData(gameJoinCode, createLobbySetting);
            await _connectionManager.CreateLobbyAsync(createLobbyData);

            _createPanel.Close();
        }

        private async void JoinLobby(string joinCode)
        {
            var joinLobbyData = new JoinLobbyData(joinCode);
            await _connectionManager.JoinLobbyAsync(joinLobbyData);

            var gameJoinCode = _connectionManager.CurrentLobby.Data[nameof(CreateLobbyData.GameJoinCode)].Value;
            await _connectionManager.JoinGameAsync(gameJoinCode);

            _joinPanel.Close();
        }

        private void OpenCreateLobbyPanel()
        {
            _createPanel.Open();
            _connectionPanel.Close();
        }

        private void OpenJoinLobbyPanel()
        {
            _joinPanel.Open();
            _connectionPanel.Close();
        }

        private void ExitToMenu()
        {
            _sceneLoader.LoadAsync(SceneNames.MainMenuScene).Forget();
        }

        private void CloseCreatePanel()
        {
            _createPanel.Close();
            _connectionPanel.Open();
        }

        private void CloseJoinPanel()
        {
            _joinPanel.Close();
            _connectionPanel.Open();
        }
    }
}