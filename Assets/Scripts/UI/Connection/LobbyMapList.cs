using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Connection
{
    public class LobbyMapList : MonoBehaviour
    {
        [SerializeField] private LobbyMapView[] _mapViews;

        public event Action<string> MapSelected;

        public void Initialize(IEnumerable<string> mapNames)
        {
            var viewConfigPairs = _mapViews.Zip(mapNames, Tuple.Create);
            foreach (var (weaponView, mapName) in viewConfigPairs)
            {
                weaponView.Initialize(mapName);
                weaponView.MapSelected += RaiseMapSelected;
            }
        }

        private void OnDestroy()
        {
            foreach (var weaponView in _mapViews)
                weaponView.MapSelected -= RaiseMapSelected;
        }

        private void RaiseMapSelected(string mapName)
        {
            MapSelected?.Invoke(mapName);
        }

    }
}