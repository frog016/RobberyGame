using Unity.Netcode;
using UnityEngine;

namespace Structure.Netcode
{
    public class ServerBehaviour : NetworkBehaviour
    {
        protected virtual bool ShouldDisableIfNotServer => true;

        public override void OnNetworkSpawn()
        {
            enabled = !ShouldDisableIfNotServer || IsServer;
            if (IsServer == false)
                return;

            OnServerNetworkSpawn();
        }

        public override void OnNetworkDespawn()
        {
            if (IsServer == false)
                return;

            OnServerNetworkDespawn();
        }

        private void Update()
        {
            if (IsServer == false || IsSpawned == false)
                return;

            OnServerUpdate();
        }

        private void FixedUpdate()
        {
            if (IsServer == false || IsSpawned == false)
                return;

            OnServerFixedUpdate();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsServer == false || IsSpawned == false || IsServer == false)
                return;

            OnServerTriggerEnter2D(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (IsServer == false || IsSpawned == false)
                return;

            OnServerTriggerStay2D(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsServer == false || IsSpawned == false)
                return;

            OnServerTriggerExit2D(other);
        }

        protected virtual void OnServerNetworkSpawn() { }
        protected virtual void OnServerNetworkDespawn() { }
        protected virtual void OnServerUpdate() { }
        protected virtual void OnServerFixedUpdate() { }
        protected virtual void OnServerTriggerEnter2D(Collider2D other) { }
        protected virtual void OnServerTriggerStay2D(Collider2D other) { }
        protected virtual void OnServerTriggerExit2D(Collider2D other) { }
    }
}