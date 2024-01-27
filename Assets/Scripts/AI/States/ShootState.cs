using Entity;
using Entity.Attack;
using System;
using UnityEngine;

namespace AI.States
{
    public class ShootState : CharacterState, IEnterState<Func<Vector2>>, IUpdateState
    {
        private Gun ActiveGun => Context.AttackBehaviour.Gun;
        private Func<Vector2> _shootDirectionProvider;

        public ShootState(Character context) : base(context)
        {
        }

        public void Enter(Func<Vector2> shootDirectionProvider)
        {
            _shootDirectionProvider = shootDirectionProvider;
        }

        public void Update()
        {
            try
            {
                Context.StateMachine.GetState<WalkState>().Update();
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (ActiveGun.Cooldown.IsReady)
                {
                    var direction = _shootDirectionProvider.Invoke();
                    ActiveGun.Shoot(direction);
                }
            }
        }
    }
}