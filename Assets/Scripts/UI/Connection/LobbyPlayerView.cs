using TMPro;
using UnityEngine;

namespace UI.Connection
{
    public class LobbyPlayerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerNameText;

        public void Initialize(string playerName)
        {
            _playerNameText.text = playerName;
        }
    }
}