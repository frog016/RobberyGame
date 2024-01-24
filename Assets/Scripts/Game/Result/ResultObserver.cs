using System;
using System.Collections.Generic;
using Entity;
using Structure.Netcode;

namespace Game.Result
{
    public class ResultObserver : ServerBehaviour
    {
        public event Action<ulong, bool> PlayerGameEnded; 

        private readonly List<Character> _players = new();

        public void Add(ulong clientId, Character player)
        {
            _players.Add(player);
            player.HealthChanged += health => OnPlayerDied(health, clientId);
        }

        private void OnPlayerDied(int health, ulong clientId)
        {
            if (health <= 0)
                PlayerGameEnded?.Invoke(clientId, false);
        }
    }
}
