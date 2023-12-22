using Structure.Netcode;
using UnityEngine;

namespace Entity
{
    public class RootDetector : ServerBehaviour, IPlayerDetector
    {
        [SerializeField] private PlayerDetector _playerDetector;

        public void DetectPlayer(Character character)
        {
            _playerDetector.DetectPlayer(character);
        }

        public void UnDetectPlayer(Character character)
        {
            _playerDetector.UnDetectPlayer(character);
        }
    }
}