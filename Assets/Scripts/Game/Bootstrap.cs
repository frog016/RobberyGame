using AI.FSM;
using AI.States;
using Creation.Factory;
using Creation.Pool;
using Game.State;
using Structure.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        private GameStateMachine _gameStateMachine;
        private IServiceLocator _serviceLocator;

        private void Awake()
        {
            _serviceLocator = new ServiceLocator();

            var stateMachineImplementation = CreateGameStateMachineImplementation();

            _gameStateMachine = new GameStateMachine(stateMachineImplementation);
            _gameStateMachine.SetState<StealthGameState>();

            Register();
        }

        private void Register()
        {
            _serviceLocator.Register(_gameStateMachine);

            var factory = new UnityFactory();
            _serviceLocator.Register<IFactory>(factory);

            var poolProvider = new ProjectilePoolProvider(factory);
            _serviceLocator.Register(poolProvider);

            var gunFactory = new GunFactory(poolProvider);
            _serviceLocator.Register(gunFactory);
        }

        private void Start()
        {
            LoadNextScene();
        }

        private void LoadNextScene()
        {
            SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
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
