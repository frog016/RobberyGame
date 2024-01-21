using System.Threading.Tasks;
using AI.FSM;
using AI.States;
using Creation.Factory;
using Creation.Pool;
using Cysharp.Threading.Tasks;
using Game.Connection;
using Game.Connection.Lobbies;
using Game.State;
using Structure.Scene;
using Structure.Service;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Lobbies;
using Unity.Services.Relay;
using UnityEngine;

namespace Game
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private ConnectionManager _connectionManager;
        [SerializeField] private NetworkManager _networkManager;

        private IServiceLocator _serviceLocator;

        private async void Awake()
        {
            _serviceLocator = new ServiceLocator();
            await Register();

            ConstructConnectionManager();
            LoadNextScene();
        }

        private async Task Register()
        {
            var authService = new UnityAuthenticationService();
            await authService.AuthenticateAsync();
            _serviceLocator.Register<IAuthenticationService>(authService);

            var gameStateMachine = new GameStateMachine(CreateGameStateMachineImplementation());
            gameStateMachine.SetState<StealthGameState>();
            _serviceLocator.Register(gameStateMachine);

            var sceneLoader = new UnitySceneLoader(NetworkManager.Singleton);
            _serviceLocator.Register<ISceneLoader>(sceneLoader);

            var factory = new UnityFactory();
            _serviceLocator.Register<IFactory>(factory);

            var poolProvider = new ProjectilePoolProvider(factory);
            _serviceLocator.Register(poolProvider);

            var gunFactory = new GunFactory(poolProvider);
            _serviceLocator.Register(gunFactory);
        }

        private void ConstructConnectionManager()
        {
            var unityTransport = _networkManager.GetComponent<UnityTransport>();
            var gameConnectionCreator = new RelayGameConnectionCreator(RelayService.Instance, _networkManager, unityTransport);

            var authenticationService = ServiceLocator.Instance.Get<IAuthenticationService>();
            var lobbyConnection = new UnityLobbyServiceConnection(LobbyService.Instance, authenticationService);

            _connectionManager.Constructor(gameConnectionCreator, lobbyConnection);
        }

        private void LoadNextScene()
        {
            var sceneLoader = _serviceLocator.Get<ISceneLoader>();
            sceneLoader.LoadAsync(SceneNames.MainMenuScene).Forget();
        }

        private static IStateMachine CreateGameStateMachineImplementation()
        {
            var states = new IState[]
            {
                new StealthGameState(),
                new BattleGameState()
            };

            return new StateMachine(states);
        }
    }
}
