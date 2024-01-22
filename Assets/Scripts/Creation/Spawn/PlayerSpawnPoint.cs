﻿using Config;
using Creation.Factory;
using Entity;
using InputSystem;
using Presenter;
using Structure.Netcode;
using Structure.Service;
using Unity.Netcode;
using UnityEngine;

namespace Creation.Spawn
{
    public class PlayerSpawnPoint : ServerBehaviour
    {
        [SerializeField] private Character _playerPrefab;
        [SerializeField] private CharacterConfig _playerConfig;
        [SerializeField] private GunConfig _startGunConfig;
        [SerializeField] private NetworkPlayerInput _networkInputPrefab;

        private GunFactory _gunFactory;

        protected override void OnServerNetworkSpawn()
        {
            _gunFactory = ServiceLocator.Instance.Get<GunFactory>();
            var factory = ServiceLocator.Instance.Get<IFactory>();

            foreach (var connectedClientId in NetworkManager.ConnectedClientsIds)
            {
                var playerInput = GetPlayerInput(connectedClientId);
                var characterStateMachineFactories = new ICharacterStateMachineFactory[]
                {
                    new PlayerStateMachineFactory(playerInput),
                    new PoliceStateMachineFactory()
                };

                var characterFactory = new CharacterFactory(factory, characterStateMachineFactories);

                Spawn(connectedClientId, characterFactory);
            }
        }

        private IPlayerInput GetPlayerInput(ulong connectedClientId)
        {
            var playerInput = Instantiate(_networkInputPrefab);
            playerInput.GetComponent<NetworkObject>().SpawnWithOwnership(connectedClientId);

            return playerInput;
        }

        public void Spawn(ulong connectedClientId, CharacterFactory characterFactory)
        {
            var player = characterFactory.CreatePlayer(connectedClientId, _playerPrefab, _playerConfig);
            player.Position = transform.position;

            var gun = _gunFactory.Create(_startGunConfig, player);
            player.AttackBehaviour.Initialize(gun);

            var presenterRoot = player.GetComponentInChildren<PresenterRoot>();
            presenterRoot.Initialize();
        }
    }
}
