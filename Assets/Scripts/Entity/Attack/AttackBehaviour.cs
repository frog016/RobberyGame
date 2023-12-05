using UnityEngine;

namespace Entity.Attack
{
    public class AttackBehaviour
    {
        public readonly Gun Gun;

        public AttackBehaviour(Gun gun)
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