using Entity;
using Game.State;
using InputSystem;
using UnityEngine;

namespace AI.States
{
    public class BattleModeState : CharacterState, IEnterState
    {
        private readonly IPlayerInput _input;

        public BattleModeState(Character context, IPlayerInput input) : base(context)
        {
            _input = input;
        }

        public void Enter()
        {
            Debug.Log($"Battle mode launched");
            _input.CharacterBattleMode.Enable();

            if (GameStateMachine.Instance.Current is not BattleGameState)
                GameStateMachine.Instance.SetState<BattleGameState, Character>(Context);

            Ended = true;
        }
    }
}