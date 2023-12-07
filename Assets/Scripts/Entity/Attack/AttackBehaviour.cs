using UnityEngine;

namespace Entity.Attack
{
    public class AttackBehaviour
    {
        public Gun Gun;

        public void Initialize(Gun gun)
        {
            Gun = gun;
        }

        public void PerformAttack(Vector2 direction)
        {
            if (Gun.Cooldown.IsReady == false)
                return;

            Gun.Shoot(direction);
        }
    }
}