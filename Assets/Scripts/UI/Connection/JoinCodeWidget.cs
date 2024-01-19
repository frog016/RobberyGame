using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.Connection
{
    public class JoinCodeWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _joinCodeText;
        [SerializeField] private Button _copyCodeButton;

        private void OnEnable()
        {
            _copyCodeButton.onClick.AddListener(CopyCodeToClipboard);
        }

        private void OnDisable()
        {
            _copyCodeButton.onClick.RemoveListener(CopyCodeToClipboard);
        }

        public void SetJoinCode(string joinCode)
        {
            _joinCodeText.text = joinCode;
        }

        private void CopyCodeToClipboard()
        {
            var code = _joinCodeText.text;
            code.CopyToClipboard();
        }
    }
}