using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Connection
{
    public class LobbyMapView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _mapNameText;
        [SerializeField] private Button _selectButton;

        public event Action<string> MapSelected;
        
        private string _mapName;

        public void Initialize(string mapName, string mapViewName)
        {
            _mapName = mapName;
            _mapNameText.text = mapViewName;

            _selectButton.onClick.AddListener(RaiseMapSelected);
        }

        private void OnDestroy()
        {
            _selectButton.onClick.RemoveListener(RaiseMapSelected);
        }

        private void RaiseMapSelected()
        {
            MapSelected?.Invoke(_mapName);
        }
    }
}