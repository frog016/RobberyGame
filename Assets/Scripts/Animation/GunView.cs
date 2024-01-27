using Entity;
using Entity.Attack;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Animation
{
    public class GunView : MonoBehaviour
    {
        [SerializeField] private Transform _muzzleTransform;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Camera _camera;
        private Character _character;

        public Vector2 MuzzlePositionLocal => _muzzleTransform.localPosition;

        public void Initialize(Character character, Camera camera)
        {
            _character = character;
            _camera = camera;
        }

        private void Update()
        {
            if (_character == null)
                return;

            if (_character.TeamId == TeamId.Player)
                RotatePlayerWeapon();
            else if (_character.TeamId == TeamId.Police)
                RotatePoliceWeapon();

            FlipSprite();
        }

        private void RotatePlayerWeapon()
        {
            if (_camera == null)
                return;

            var direction = GetMouseDirection();
            transform.right = direction;
        }

        private void RotatePoliceWeapon()
        {
            var direction = _character.Rotation;
            transform.right = direction;
        }

        private void FlipSprite()
        {
            _spriteRenderer.flipY = transform.rotation.eulerAngles.z is > 90 and < 270;
        }

        private Vector2 GetMouseDirection()
        {
            var mousePosition = Mouse.current.position.value;
            var worldPosition = _camera.ScreenToWorldPoint(mousePosition);

            return (worldPosition - transform.position).normalized;
        }
    }
}