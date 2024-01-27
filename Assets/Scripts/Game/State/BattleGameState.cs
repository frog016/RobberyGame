using System.Collections.Generic;
using System.Linq;
using AI.States;
using AI.States.NPC;
using Entity;
using Entity.Attack;
using Unity.Netcode;
using UnityEngine;

namespace Game.State
{
    public class BattleGameState : GameState, IEnterState<Character>
    {
        public void Enter(Character character)
        {
            var security = GetAllSecurity();

            foreach (var securityGuard in security)
                securityGuard.StateMachine.SetState<ChaseState, Character>(character);

            foreach (var player in GetPlayers())
            {
                player.StateMachine.SetState<BattleModeState>();
            }
        }

        private static IEnumerable<Character> GetAllSecurity()
        {
            return Object
                .FindObjectsOfType<Character>()
                .Where(character => character.TeamId == TeamId.Police);
        }

        private static IEnumerable<Character> GetPlayers()
        {
            return NetworkManager.Singleton.ConnectedClients
                .Select(client => client.Value.PlayerObject)
                .Select(playerObject => playerObject.GetComponent<Character>());
        }
    }
}