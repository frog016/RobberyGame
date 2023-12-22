using AI.FSM;
using AI.States;
using Game.State;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            var stateMachineImplementation = CreateGameStateMachineImplementation();

            _gameStateMachine = new GameStateMachine(stateMachineImplementation);
            _gameStateMachine.SetState<StealthGameState>();
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
