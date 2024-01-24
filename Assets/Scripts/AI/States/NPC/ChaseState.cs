using Entity;
using Entity.Movement;
using Interactable.Camera;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States.NPC
{
    public class ChaseState : CharacterState, IEnterState<Character>, IUpdateState, IExitState
    {
        public Character Target { get; private set; }
        public readonly float AttackDistance;

        private const float DistanceEpsilon = 1e-2f;

        public ChaseState(Character context, float attackDistance) : base(context)
        {
            AttackDistance = attackDistance;
        }

        public void Enter(Character target)
        {
            SetAgentMovement();
            DisableFieldOfView();

            Ended = false;
            Target = target;
        }

        public void Update()
        {
            if (Target == null)
            {
                Ended = true;
                return;
            }

            ChaseTarget();

            Ended = Vector2.Distance(Context.Position, Target.Position) < DistanceEpsilon;
        }

        public void Exit()
        {
            Context.GetComponent<NavMeshAgent>().isStopped = true;
        }

        private void ChaseTarget()
        {
            Context.Movement.Move(Target.Position);
        }

        private void SetAgentMovement()
        {
            var navMeshAgent = Context.GetComponent<NavMeshAgent>();
            navMeshAgent.isStopped = false;
            
            if (Context.Movement is not NavMeshMovement)
                Context.Movement = new NavMeshMovement(navMeshAgent, Context.Movement.Speed);
        }

        private void DisableFieldOfView()
        {
            var fieldOfView = Context.GetComponent<FieldOfView>();
            fieldOfView.enabled = false;
            fieldOfView.MeshSource.gameObject.SetActive(false);
        }
    }
}