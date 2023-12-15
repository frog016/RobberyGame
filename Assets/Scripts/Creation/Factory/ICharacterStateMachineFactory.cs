using AI.FSM;
using Config;
using Entity;
using Entity.Attack;

namespace Creation.Factory
{
    public interface ICharacterStateMachineFactory
    {
        TeamId CharacterTeamId { get; }
        IStateMachine CreateStateMachine(Character context, CharacterConfig config, object extraArgument = null);
    }
}