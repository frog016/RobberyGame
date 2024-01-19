using AI.States.NPC;
using Config;
using Creation.Factory;
using Entity;
using Structure.Netcode;
using Structure.Service;
using UnityEngine;
using UnityEngine.AI;

namespace Creation.Spawn
{
    public class PoliceSpawnPoint : ServerBehaviour
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private GunConfig _startGunConfig;
        [SerializeField] private PatrolWay _patrolWay;

        private GunFactory _gunFactory;
        private CharacterFactory _characterFactory;

        protected override void OnServerNetworkSpawn()
        {
            var factory = ServiceLocator.Instance.Get<IFactory>();
            _gunFactory = ServiceLocator.Instance.Get<GunFactory>();

            var characterStateMachineFactories = new ICharacterStateMachineFactory[]
            {
                new PoliceStateMachineFactory()
            };

            _characterFactory = new CharacterFactory(factory, characterStateMachineFactories);

            Spawn();
        }

        public void Spawn()
        {
            var character = _characterFactory.CreatePolice(NetworkManager.LocalClientId, _characterPrefab, _characterConfig, _patrolWay);
            character.Position = transform.position;

            var gun = _gunFactory.Create(_startGunConfig, character);
            character.AttackBehaviour.Initialize(gun);

            character.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}