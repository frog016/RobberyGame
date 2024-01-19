using Cinemachine;
using Config;
using Creation.Factory;
using Entity;
using Structure.Netcode;
using Structure.Service;
using UnityEngine;

namespace Creation.Spawn
{
    public class PlayerSpawnPoint : ServerBehaviour
    {
        [SerializeField] private Character _playerPrefab;
        [SerializeField] private CharacterConfig _playerConfig;
        [SerializeField] private GunConfig _startGunConfig;
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        private GunFactory _gunFactory;
        private CharacterFactory _characterFactory;

        protected override void OnServerNetworkSpawn()
        {
            var playerInput = new PlayerInput();
            playerInput.CharacterBaseMode.Enable();
            playerInput.CharacterStealthMode.Enable();
            playerInput.CharacterBattleMode.Disable();

            var characterStateMachineFactories = new ICharacterStateMachineFactory[]
            {
                new PlayerStateMachineFactory(playerInput, _camera),
                new PoliceStateMachineFactory()
            };

            var factory = ServiceLocator.Instance.Get<IFactory>();
            _characterFactory = new CharacterFactory(factory, characterStateMachineFactories);

            _gunFactory = ServiceLocator.Instance.Get<GunFactory>();

            Spawn();
        }

        public void Spawn()
        {
            var player = _characterFactory.CreatePlayer(NetworkManager.LocalClientId, _playerPrefab, _playerConfig);
            player.Position = transform.position;

            var gun = _gunFactory.Create(_startGunConfig, player);
            player.AttackBehaviour.Initialize(gun);

            _virtualCamera.Follow = player.transform;
            _virtualCamera.LookAt = player.transform;
        }
    }
}
