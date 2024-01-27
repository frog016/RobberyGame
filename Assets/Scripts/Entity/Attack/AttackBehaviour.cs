using Structure.Netcode;
using System;
using UnityEngine;
using UnityEngine.Serialization;

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