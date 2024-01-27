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

        private Vector3 _lastPosition;

        protected override void OnServerNetworkSpawn()
        {
            _lastPosition = transform.position;
            _character.Movement.Moved += OnCharacterMoved;
        }

        protected override void OnServerNetworkDespawn()
        {
            _character.Movement.Moved -= OnCharacterMoved;
        }

        private void OnCharacterMoved(Vector2 direction)
        {
            //_animator.SetFloat(MoveXHash, direction.x);
            //_animator.SetFloat(MoveYHash, direction.y);
            //_animator.SetBool(IsMovingHash, true);
        }

        private void FixedUpdate()
        {
            if (IsServer == false)
                return;

            var isMove = Vector3.Distance(_lastPosition, transform.position) > 1e-4;
            var moveX = isMove ? _character.Rotation.x : 0;
            var moveY = isMove ? _character.Rotation.y : 0;

            _animator.SetFloat(MoveXHash, moveX);
            _animator.SetFloat(MoveYHash, moveY);
            _animator.SetBool(IsMovingHash, isMove);

            _lastPosition = transform.position;
        }
    }
}