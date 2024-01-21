using Game.Connection;
using Presenter;
using Structure.Scene;
using Structure.Service;
using UnityEngine;

namespace Game.Construct
{
    public class LobbyConstructor : MonoBehaviour
    {
        [SerializeField] private ConnectionManagerPresenter _connectionManagerPresenter;
        [SerializeField] private LobbyPresenter _lobbyPresenter;

        private void Awake()
        {
            var connectionManager = FindObjectOfType<ConnectionManager>();
            var sceneLoader = ServiceLocator.Instance.Get<ISceneLoader>();

            _connectionManagerPresenter.Constructor(sceneLoader, connectionManager);
            _lobbyPresenter.Constructor(sceneLoader, connectionManager);
        }
    }
}