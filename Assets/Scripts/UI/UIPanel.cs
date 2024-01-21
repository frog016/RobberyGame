using System;
using UI.Connection;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] private bool _disableOnStart = true;

        public bool IsActive => gameObject.activeSelf;

        protected virtual bool DisableOnStart => _disableOnStart;

        private Action _closeCallback;

        private void Start()
        {
            if (DisableOnStart)
                Close();
        }

        public void Open(Action closeCallback = null)
        {
            gameObject.SetActive(true);
            _closeCallback = closeCallback;
        }

        public void Close()
        {
            gameObject.SetActive(false);
            _closeCallback?.Invoke();
        }
    }
}