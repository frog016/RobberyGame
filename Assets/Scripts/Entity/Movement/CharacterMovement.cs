using UnityEngine;

namespace Entity.Movement
{
    public class CharacterMovement : IMovement
    {
        public float Speed { get; set; }

        private readonly Character _character;

        public CharacterMovement(Character character, float speed)
        {
            _character = character;
            Speed = speed;
        }

        public void Move(Vector2 direction)
        {
            _character.Position += direction * Speed * Time.deltaTime;
            _character.Rotation = direction;
        }
    }
}