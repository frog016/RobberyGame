using Entity;
using Game.State;
using UnityEngine;

namespace AI.States
{
    public class BattleModeState : CharacterState, IEnterState
    {
        private readonly PlayerInput _input;

        public BattleModeState(Character context, PlayerInput input) : base(context)
        {
            _input = input;
        }

        public void Enter()
        {
            Debug.Log($"Battle mode launched");
            GameStateMachine.Instance.SetState<BattleGameState, Character>(Context);
            _input.CharacterBattleMode.Enable();

            Ended = true;
        }
    }
}