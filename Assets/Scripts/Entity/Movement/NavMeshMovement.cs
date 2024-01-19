﻿using UnityEngine;
using UnityEngine.AI;

namespace Entity.Movement
{
    public class NavMeshMovement : IMovement
    {
        public float Speed { get => _agent.speed; set => _agent.speed = value; }

        private readonly NavMeshAgent _agent;

        public NavMeshMovement(NavMeshAgent agent, float speed)
        {
            _agent = agent;
            _agent.speed = speed;
        }

        public void Move(Vector2 destination)
        {
            _agent.SetDestination(destination);
        }
    }
}