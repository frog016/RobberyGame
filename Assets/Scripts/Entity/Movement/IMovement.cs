using System;
using UnityEngine;

namespace Entity.Movement
{
    public interface IMovement
    {
        float Speed { get; set; }
        event Action<Vector2> Moved; 
        void Move(Vector2 direction);
    }
}
