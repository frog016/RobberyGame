using System.Collections.Generic;
using Entity;
using Entity.Attack;
using Entity.Movement;
using Interactable.Electricity;
using UnityEngine;

namespace Interactable.Camera
{
    [RequireComponent(typeof(Collider2D), typeof(FieldOfView))]
    public class SurveillanceCamera : ElectricalInteractableObject
    {
        public readonly List<Character> DetectedPlayers = new();
        public bool PlayerDetected => DetectedPlayers.Count > 0;

        private Collider2D _collider;
        private FieldOfView _fieldOfView;

        public override void Interact(Character character) { }

        protected override void OnServerNetworkSpawn()
        {
            base.OnServerNetworkSpawn();

            _collider = GetComponent<Collider2D>();
            _fieldOfView = GetComponent<FieldOfView>();
        }

        protected override void OnCharacterTriggerEnter(Character character)
        {
            if (character.TeamId == TeamId.Player)
                DetectedPlayers.Add(character);
        }

        protected override void OnCharacterTriggerExit(Character character)
        {
            if (character.TeamId == TeamId.Player)
                DetectedPlayers.Remove(character);
        }

        protected override void OnElectricityEnabled()
        {
            base.OnElectricityEnabled();

            _collider.enabled = true;
            _fieldOfView.enabled = true;
            _fieldOfView.SetFieldOfViewVisible(true);
        }

        protected override void OnElectricityDisabled()
        {
            base.OnElectricityDisabled();

            _collider.enabled = false;
            _fieldOfView.enabled = false;
            _fieldOfView.SetFieldOfViewVisible(false);

            DetectedPlayers.Clear();
        }
    }
}
