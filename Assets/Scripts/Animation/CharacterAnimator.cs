using Entity;
using Structure.Netcode;
using UnityEngine;

namespace Animation
{
    public class CharacterAnimator : ServerBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Character _character;

        private static readonly int MoveXHash = Animator.StringToHash("MoveX");
        private static readonly int MoveYHash = Animator.StringToHash("MoveY");
        private static readonly int IsMovingHash = Animator.StringToHash("IsMove");

        protected override void OnServerNetworkSpawn()
        {
            _character.Movement.Moved += OnCharacterMoved;
        }

        protected override void OnServerNetworkDespawn()
        {
            _character.Movement.Moved -= OnCharacterMoved;
        }

        private void OnCharacterMoved(Vector2 direction)
        {
            _animator.SetFloat(MoveXHash, direction.x);
            _animator.SetFloat(MoveYHash, direction.y);
            _animator.SetBool(IsMovingHash, true);
        }

        private void FixedUpdate()
        {
            if (IsServer == false)
                return;

            _animator.SetFloat(MoveXHash, 0);
            _animator.SetFloat(MoveYHash, 0);
            _animator.SetBool(IsMovingHash, false);
        }
    }
}