using Entity;
using Game.State;
using UnityEngine;

namespace AI.States
{
    public class BattleModeState : CharacterState, IEnterState
    {
        public BattleModeState(Character context) : base(context)
        {
        }

        public void Enter()
        {
            Debug.Log($"Battle mode launched");
            GameStateMachine.Instance.SetState<BattleGameState, Character>(Context);
            Ended = true;
        }
    }
}