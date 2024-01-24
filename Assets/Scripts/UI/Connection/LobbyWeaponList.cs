using System;
using System.Collections.Generic;
using System.Linq;
using Config;
using UnityEngine;

namespace UI.Connection
{
    public class LobbyWeaponList : MonoBehaviour
    {
        [SerializeField] private LobbyWeaponView[] _weaponViews;

        public event Action<GunConfig> WeaponSelected; 

        public void Initialize(IEnumerable<GunConfig> configs)
        {
            var viewConfigPairs = _weaponViews.Zip(configs, Tuple.Create);
            foreach (var (weaponView, config) in viewConfigPairs)
            {
                weaponView.Initialize(config);
                weaponView.TakeButtonClicked += RaiseWeaponSelected;
            }
        }

        private void OnDestroy()
        {
            foreach (var weaponView in _weaponViews)
                weaponView.TakeButtonClicked -= RaiseWeaponSelected;
        }

        private void RaiseWeaponSelected(GunConfig config)
        {
            WeaponSelected?.Invoke(config);
        }
    }
}