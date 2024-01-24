using System;
using Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Connection
{
    public class LobbyWeaponView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _damageText;
        [SerializeField] private TextMeshProUGUI _capacityText;
        [SerializeField] private TextMeshProUGUI _rateFireText;
        [SerializeField] private TextMeshProUGUI _cooldownTime;
        [SerializeField] private Button _takeButton;

        public event Action<GunConfig> TakeButtonClicked; 

        private GunConfig _gunConfig;

        public void Initialize(GunConfig gunConfig)
        {
            _gunConfig = gunConfig;
            _takeButton.onClick.AddListener(RaiseTakeClicked);

            InitializeStats(gunConfig);
        }

        private void OnDestroy()
        {
            _takeButton.onClick.RemoveListener(RaiseTakeClicked);
        }

        private void InitializeStats(GunConfig gunConfig)
        {
            _nameText.text = _gunConfig.name;
            _damageText.text = gunConfig.ShootDamage.ToString();
            _capacityText.text = gunConfig.MagazineCapacity.ToString();
            _rateFireText.text = gunConfig.BulletLaunchDelay.ToString();
            _cooldownTime.text = gunConfig.ShootCooldown.ToString();
        }

        private void RaiseTakeClicked()
        {
            TakeButtonClicked?.Invoke(_gunConfig);
        }
    }
}