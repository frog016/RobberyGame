using Entity;
using Entity.Attack;
using Interactable.Electricity;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable.Camera
{
    public class SurveillanceCameraMonitor : ElectricalInteractableObject
    {
        [SerializeField] private SurveillanceCamera[] _surveillanceCameras;

        private readonly HashSet<Character> _detectedPlayers = new();

        public override void Interact(Character character)
        {
            switch (character.TeamId)
            {
                case TeamId.Police when HaveElectricity:
                    DetectPlayersByPolice(character);
                    break;
            }
        }

        public override void EndInteract(Character character)
        {
            switch (character.TeamId)
            {
                case TeamId.Police when HaveElectricity:
                    UnDetectPlayersByPolice(character);
                    break;
            }
        }

        protected override void OnServerNetworkSpawn()
        {
            base.OnServerNetworkSpawn();

            foreach (var surveillanceCamera in _surveillanceCameras)
            {
                surveillanceCamera.DetectedPlayers.Added += Add;
                surveillanceCamera.DetectedPlayers.Removed += Remove;
            }
        }

        protected override void OnServerNetworkDespawn()
        {
            base.OnServerNetworkDespawn();

            foreach (var surveillanceCamera in _surveillanceCameras)
            {
                surveillanceCamera.DetectedPlayers.Added -= Add;
                surveillanceCamera.DetectedPlayers.Removed -= Remove;
            }
        }

        private void DetectPlayersByPolice(Character police)
        {
            var playerDetector = police.GetComponent<IPlayerDetector>();

            foreach (var detectedPlayer in _detectedPlayers)
                playerDetector.DetectPlayer(detectedPlayer);
        }

        private void UnDetectPlayersByPolice(Character police)
        {
            var playerDetector = police.GetComponent<IPlayerDetector>();

            foreach (var detectedPlayer in _detectedPlayers)
                playerDetector.UnDetectPlayer(detectedPlayer);
        }

        private void Add(Character character) { _detectedPlayers.Add(character); }
        private void Remove(Character character) { _detectedPlayers.Remove(character); }
    }
}