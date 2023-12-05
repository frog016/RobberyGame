using Entity.Health;
using Entity.Movement;

namespace Entity.Attack
{
    public interface ITarget : IDamageable, ITransformable
    {
        TeamId TeamId { get; }
    }
}