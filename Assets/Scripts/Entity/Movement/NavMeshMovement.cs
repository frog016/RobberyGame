using System;
using UnityEngine;
using UnityEngine.AI;

namespace Entity.Movement
{
    public class NavMeshMovement : IMovement
    {
        public float Speed { get => _agent.speed; set => _agent.speed = value; }
        public event Action<Vector2> Moved;

        private readonly NavMeshAgent _agent;
        private readonly ITransformable _transformable;

        public NavMeshMovement(ITransformable transformable, NavMeshAgent agent, float speed)
        {
            _transformable = transformable;
            _agent = agent;
            _agent.speed = speed;
        }

        public void Move(Vector2 destination)
        {
            var direction = ((Vector3)destination - _agent.transform.position).normalized;

            _agent.SetDestination(destination);
            _transformable.Rotation = direction;

            Moved?.Invoke(direction);
        }
    }
}