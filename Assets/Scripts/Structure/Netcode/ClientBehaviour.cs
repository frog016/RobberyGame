using Unity.Netcode;
using UnityEngine;

namespace Structure.Netcode
{
    public class ClientBehaviour : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            enabled = IsClient;
            if (IsClient == false)
                return;

            OnClientNetworkSpawn();
        }

        public override void OnNetworkDespawn()
        {
            if (IsClient == false)
                return;

            OnClientNetworkDespawn();
        }

        private void Update()
        {
            if (IsSpawned == false)
                return;

            OnClientUpdate();
        }

        private void FixedUpdate()
        {
            if (IsSpawned == false)
                return;

            OnClientFixedUpdate();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsSpawned == false || IsClient == false)
                return;

            OnClientTriggerEnter2D(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (IsSpawned == false || IsClient == false)
                return;

            OnClientTriggerStay2D(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsSpawned == false || IsClient == false)
                return;

            OnClientTriggerExit2D(other);
        }

        protected virtual void OnClientNetworkSpawn() { }
        protected virtual void OnClientNetworkDespawn() { }
        protected virtual void OnClientUpdate() { }
        protected virtual void OnClientFixedUpdate() { }
        protected virtual void OnClientTriggerEnter2D(Collider2D other) { }
        protected virtual void OnClientTriggerStay2D(Collider2D other) { }
        protected virtual void OnClientTriggerExit2D(Collider2D other) { }
    }
}