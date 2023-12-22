using AI.States;

namespace Game.State
{
    public abstract class GameState : IState
    {
        public bool Ended { get; protected set; }
    }
}