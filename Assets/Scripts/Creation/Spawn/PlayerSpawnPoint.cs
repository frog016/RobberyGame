using Config;
using Creation.Factory;
using Creation.Pool;
using Entity;
using Structure.Netcode;
using UnityEngine;

namespace Creation.Spawn
{
    public class PlayerSpawnPoint : ServerBehaviour
    {
        [SerializeField] private Character _playerPrefab;
        [SerializeField] private CharacterConfig _playerConfig;
        [SerializeField] private GunConfig _startGunConfig;
        [SerializeField] private Camera _camera;

        private PlayerInput _playerInput;
        private IFactory _factory;
        private ProjectilePoolProvider _projectilePoolProvider;
        private GunFactory _gunFactory;
        private CharacterFactory _characterFactory;

        protected override void OnServerNetworkSpawn()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();

            _factory = new UnityFactory();
            _projectilePoolProvider = new ProjectilePoolProvider(_factory);
            _gunFactory = new GunFactory(_projectilePoolProvider);

            var characterStateMachineFactories = new ICharacterStateMachineFactory[]
            {
                new PlayerStateMachineFactory(_playerInput, _camera),
            };

            _characterFactory = new CharacterFactory(_factory, characterStateMachineFactories);

            Spawn();
        }

        public void Spawn()
        {
            var player = _characterFactory.CreatePlayer(NetworkManager.LocalClientId, _playerPrefab, _playerConfig);
            var gun = _gunFactory.Create(_startGunConfig, player);

            player.AttackBehaviour.Initialize(gun);
        }
    }
}
