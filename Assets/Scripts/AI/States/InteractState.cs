using Entity;
using UnityEngine;

namespace AI.States
{
    public class InteractState : CharacterState, IEnterState
    {
        public InteractState(Character character) : base(character)
        {
        }

        public void Enter()
        {
            Ended = true;
            Debug.Log("Interacted");
        }
    }
}