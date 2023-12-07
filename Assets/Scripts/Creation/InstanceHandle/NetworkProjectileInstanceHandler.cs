using Creation.Pool;
using Entity.Attack;
using Unity.Netcode;
using UnityEngine;

namespace Creation.InstanceHandle
{
    public class NetworkProjectileInstanceHandler : INetworkPrefabInstanceHandler
    {
        private readonly ProjectilePool _pool;

        public NetworkProjectileInstanceHandler(ProjectilePool pool)
        {
            _pool = pool;
        }

        public NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
        {
            var instance = _pool.Get();
            instance.transform.SetPositionAndRotation(position, rotation);

            var networkObject = instance.GetComponent<NetworkObject>();
            if (networkObject.OwnerClientId != ownerClientId)
                networkObject.ChangeOwnership(ownerClientId);

            return networkObject;
        }

        public void Destroy(NetworkObject networkObject)
        {
            if (networkObject.NetworkManager.IsServer) 
                return;

            var projectile = networkObject.GetComponent<Projectile>();
            _pool.Release(projectile);

            Debug.Log($"{projectile.name}:{projectile.NetworkObjectId} released in pool on Client.");
        }
    }
}