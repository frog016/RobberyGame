using UnityEngine;

namespace Entity.Attack
{
    public class AttackBehaviour
    {
        private readonly Gun _gun;

        public AttackBehaviour(Gun gun)
        {
            _gun = gun;
        }

        public void PerformAttack(Vector2 direction)
        {
            if (_gun.Cooldown.IsReady == false)
                return;

            _gun.Shoot(direction);
        }
    }
}