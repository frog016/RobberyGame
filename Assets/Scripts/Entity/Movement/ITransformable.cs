using UnityEngine;

namespace Entity.Movement
{
    public interface ITransformable
    {
        Vector2 Position { get; set; }
        Vector2 Rotation { get; set; }
    }
}