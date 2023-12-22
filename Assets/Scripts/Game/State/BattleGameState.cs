using System.Collections.Generic;
using System.Linq;
using AI.States;
using AI.States.NPC;
using Entity;
using Entity.Attack;
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
        }

        private static IEnumerable<Character> GetAllSecurity()
        {
            return Object
                .FindObjectsOfType<Character>()
                .Where(character => character.TeamId == TeamId.Police);
        }
    }
}