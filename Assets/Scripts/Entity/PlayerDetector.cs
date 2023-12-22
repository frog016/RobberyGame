using System.Collections.Generic;
using System.Linq;
using Entity.Attack;
using Structure.Netcode;
using UnityEngine;

namespace Entity
{
    public class PlayerDetector : ServerBehaviour, IPlayerDetector
    {
        [SerializeField] private float _insideDetectAreaTime;

        private readonly Dictionary<Character, float> _detectedPlayerTimers = new();

        public void DetectPlayer(Character character)
        {
            _detectedPlayerTimers.Add(character, _insideDetectAreaTime);
        }

        public void UnDetectPlayer(Character character)
        {
            _detectedPlayerTimers.Remove(character);
        }

        protected override void OnServerFixedUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;

            foreach (var player in _detectedPlayerTimers.Keys.ToArray())
            {
                _detectedPlayerTimers[player] -= fixedDeltaTime;

                if (_detectedPlayerTimers[player] <= 0)
                    RaiseAlert(player);
            }
        }

        protected override void OnServerTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Character>(out var character) && character.TeamId == TeamId.Player)
                DetectPlayer(character);
        }

        protected override void OnServerTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Character>(out var character) && character.TeamId == TeamId.Player)
                UnDetectPlayer(character);
        }

        private void RaiseAlert(Character character)
        {
            Debug.Log($"{character} detected!!! Alert!!! Warning!!!");

            Clear();
        }

        private void Clear()
        {
            enabled = false;
            _detectedPlayerTimers.Clear();
        }
    }
}