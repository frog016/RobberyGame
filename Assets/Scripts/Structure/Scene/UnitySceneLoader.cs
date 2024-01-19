using Cysharp.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Structure.Scene
{
    public class UnitySceneLoader : ISceneLoader
    {
        private readonly NetworkManager _networkManager;

        public UnitySceneLoader(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public async UniTask LoadAsync(string sceneName)
        {
            const LoadSceneMode loadMode = LoadSceneMode.Single;

            if (_networkManager.IsListening)
                await LoadSceneByNetworkManager(sceneName, loadMode);
            else
                await LoadSceneBySceneManager(sceneName, loadMode);

            Debug.Log($"{sceneName} was loaded.");
        }

        private UniTask LoadSceneByNetworkManager(string sceneName, LoadSceneMode loadMode)
        {
            var networkSceneManager = _networkManager.SceneManager;
            var progressStatus = networkSceneManager.LoadScene(sceneName, loadMode);

            Debug.Log($"Start loading scene {sceneName} with status {progressStatus}.");

            return UniTask.CompletedTask;
        }

        private static async UniTask LoadSceneBySceneManager(string sceneName, LoadSceneMode loadMode)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName, loadMode);
            operation.allowSceneActivation = false;

            while (operation.progress < 0.9f)
                await UniTask.Yield(cancellationToken: Application.exitCancellationToken);

            operation.allowSceneActivation = true;
        }
    }
}