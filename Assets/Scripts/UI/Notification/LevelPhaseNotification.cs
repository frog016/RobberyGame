using TMPro;
using UnityEngine;

namespace UI.Notification
{
    public class LevelPhaseNotification : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _phaseText;
        [SerializeField] private string _stealsText;
        [SerializeField] private string _battleText;

        public void SetStealthPhase()
        {
            _phaseText.text = _stealsText;
        }

        public void SetBattlePhase()
        {
            _phaseText.text = _battleText;
        }
    }
}