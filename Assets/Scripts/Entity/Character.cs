using AI.FSM;
using Entity.Health;
using Entity.Movement;
using UnityEngine;

namespace Entity
{
    public class Character : NetworkDamageable, ITransformable
    {
        public Vector2 Position { get => transform.position; set => transform.position = value; }
        public Vector2 Rotation { get; set; } = Vector2.right;

        public IMovement Movement { get; private set; }
        public IStateMachine StateMachine {get; private set; }

        public void Initialize(IMovement movement, IStateMachine stateMachine, int maxHealth)
        {
            Movement = movement;
            StateMachine = stateMachine;

            Health = maxHealth;
            MaxHealth = maxHealth;
        }

        protected override void OnServerUpdate()
        {
            StateMachine.Update();
            Debug.Log($"Character [{name}{OwnerClientId}] current state is {StateMachine.Current}.");
        }
    }
}
