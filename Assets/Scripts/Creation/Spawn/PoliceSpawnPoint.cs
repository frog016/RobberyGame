using AI.States.NPC;
using Config;
using Creation.Factory;
using Creation.Pool;
using Entity;
using Structure.Netcode;
using UnityEngine;

namespace Creation.Spawn
{
    public class PoliceSpawnPoint : ServerBehaviour
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private GunConfig _startGunConfig;
        [SerializeField] private PatrolWay _patrolWay;

        private IFactory _factory;
        private ProjectilePoolProvider _projectilePoolProvider;
        private GunFactory _gunFactory;
        private CharacterFactory _characterFactory;

        protected override void OnServerNetworkSpawn()
        {
            _factory = new UnityFactory();
            _projectilePoolProvider = new ProjectilePoolProvider(_factory);
            _gunFactory = new GunFactory(_projectilePoolProvider);

            var characterStateMachineFactories = new ICharacterStateMachineFactory[]
            {
                new PoliceStateMachineFactory()
            };

            _characterFactory = new CharacterFactory(_factory, characterStateMachineFactories);

            Spawn();
        }

        public void Spawn()
        {
            var character = _characterFactory.CreatePolice(NetworkManager.LocalClientId, _characterPrefab, _characterConfig, _patrolWay);
            character.Position = transform.position;

            var gun = _gunFactory.Create(_startGunConfig, character);
            character.AttackBehaviour.Initialize(gun);
        }
    }
}