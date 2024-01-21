using Cysharp.Threading.Tasks;
using Structure.Scene;
using UI;
using UnityEngine;

namespace Presenter
{
    public class MainMenuPresenter : MonoBehaviour
    {
        [SerializeField] private MainMenuUI _mainMenu;

        private ISceneLoader _sceneLoader;

        public void Constructor(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnEnable()
        {
            _mainMenu.StartGameButtonClicked += StartGame;
            _mainMenu.ExitButtonClicked += ExitGame;
        }

        private void OnDisable()
        {
            _mainMenu.StartGameButtonClicked -= StartGame;
            _mainMenu.ExitButtonClicked -= ExitGame;
        }

        private void StartGame()
        {
            _sceneLoader.LoadAsync(SceneNames.LobbyScene).Forget();
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}