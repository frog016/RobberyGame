using Entity;

namespace AI.Transitions.NPC
{
    public class NeedToAutoReloadCondition : CharacterStateCondition
    {
        public NeedToAutoReloadCondition(Character character) : base(character)
        {
        }

        public override bool IsHappened()
        {
            return Character.AttackBehaviour.Gun.IsMagazineEmpty();
        }
    }
}
