using System;
using Presenter;
using Structure.Scene;
using Structure.Service;
using UnityEngine;

namespace Game.Construct
{
    public class MainMenuConstructor : MonoBehaviour
    {
        [SerializeField] private MainMenuPresenter _mainMenuPresenter;

        private void Awake()
        {
            var sceneLoader = ServiceLocator.Instance.Get<ISceneLoader>();
            _mainMenuPresenter.Constructor(sceneLoader);
        }
    }
}