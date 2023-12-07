using System.Collections.Generic;
using System.Linq;
using Config;
using Entity;
using Entity.Attack;
using Entity.Movement;

namespace Creation.Factory
{
    public class CharacterFactory
    {
        private readonly IFactory _factory;
        private readonly Dictionary<TeamId, ICharacterStateMachineFactory> _characterStateMachineFactories;

        public CharacterFactory(IFactory factory, IEnumerable<ICharacterStateMachineFactory> characterStateMachineFactories)
        {
            _factory = factory;
            _characterStateMachineFactories = characterStateMachineFactories
                .ToDictionary(key => key.CharacterTeamId, value => value);
        }

        public Character CreatePlayer(ulong clientId, Character prefab, CharacterConfig config)
        {
            var playerCharacter = _factory.CreateForComponent(prefab);
            InitializeCharacter(playerCharacter, config);

            playerCharacter.NetworkObject.SpawnAsPlayerObject(clientId);
            return playerCharacter;
        }

        private void InitializeCharacter(Character character, CharacterConfig config)
        {
            var movement = new CharacterMovement(character, config.Speed);

            var stateMachineFactory = _characterStateMachineFactories[character.TeamId];
            var stateMachine = stateMachineFactory.CreateStateMachine(character, config);

            var attackBehaviour = new AttackBehaviour();

            character.Initialize(movement, stateMachine, attackBehaviour, config.MaxHealth);
        }
    }
}