using AI.FSM;
using Cinemachine;
using Entity.Attack;
using Entity.Health;
using Entity.Movement;
using UI.Connection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Entity
{
    public class Character : NetworkDamageable, ITarget
    {
        [field: SerializeField] public TeamId TeamId { get; private set; }
        [field: SerializeField] public Transform Muzzle { get; private set; }

        public Vector2 Position { get => transform.position; set => transform.position = value; }
        public Vector2 Rotation { get; set; } = Vector2.right;

        public IMovement Movement { get; set; }
        public IStateMachine StateMachine { get; private set; }
        public AttackBehaviour AttackBehaviour { get; private set; }

        public void Initialize(IMovement movement, IStateMachine stateMachine, AttackBehaviour attackBehaviour, int maxHealth)
        {
            Movement = movement;
            StateMachine = stateMachine;
            AttackBehaviour = attackBehaviour;

            Health = maxHealth;
            MaxHealth = maxHealth;
        }

        protected override void OnServerUpdate()
        {
            StateMachine.Update();
            Debug.Log($"Character [{name}{OwnerClientId}] current state is {StateMachine.Current}.");
        }

        protected override void DestroyDamageable()
        {
            if (TeamId == TeamId.Player)
            {
                DetachCamera();
            }

            NetworkObject.Despawn();
        }

        private void DetachCamera()
        {
            var cameraRoot = GetComponentInChildren<Camera>().transform.parent;
            cameraRoot.transform.SetParent(null, true);

            var virtualCamera = cameraRoot.GetComponentInChildren<CinemachineVirtualCamera>();
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;

            var winLose = GetComponentInChildren<WinLoseUIPanel>().transform.parent.parent;
            winLose.SetParent(null, true);

            EventSystem.current.transform.SetParent(null, true);
        }
    }
}
