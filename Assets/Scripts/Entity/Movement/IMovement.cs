using UnityEngine;

namespace Entity.Movement
{
    public interface IMovement
    {
        float Speed { get; set; }
        void Move(Vector2 direction);
    }
}
