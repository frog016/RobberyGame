using Entity;
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
            Ended = true;
        }
    }
}