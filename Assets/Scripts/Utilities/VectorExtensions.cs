using UnityEngine;

namespace Utilities
{
    public static class VectorExtensions
    {
        public static Vector2 SetX(this Vector2 vector, float value)
        {
            return new Vector2(value, vector.y);
        }

        public static Vector2 SetY(this Vector2 vector, float value)
        {
            return new Vector2(vector.x, value);
        }

        public static Vector2 Rotate(this Vector2 vector, float angle)
        {
            var x = Mathf.Cos(angle) * vector.x - Mathf.Sin(angle) * vector.y;
            var y = Mathf.Sin(angle) * vector.x + Mathf.Cos(angle) * vector.y;
            return new Vector2(x, y);
        }
    }
}