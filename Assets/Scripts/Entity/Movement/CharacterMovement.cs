using System.Collections.Generic;
using UnityEngine;

namespace Entity.Movement
{
    public class CharacterMovement : IMovement
    {
        public float Speed { get; set; }

        private readonly Character _character;
        private readonly Collider2D _collider;

        public CharacterMovement(Character character, float speed)
        {
            _character = character;
            _collider = character.GetComponent<Collider2D>();
            Speed = speed;
        }

        public void Move(Vector2 direction)
        {
            if (HaveObstacleInDirection(direction, direction.magnitude * Speed * Time.deltaTime))
                return;

            _character.Position += direction * Speed * Time.deltaTime;
            _character.Rotation = direction;
        }

        private bool HaveObstacleInDirection(Vector2 direction, float distance)
        {
            var castCount = _collider.Cast(direction, new ContactFilter2D(), new List<RaycastHit2D>(), distance);
            return castCount > 0;
        }
    }
}