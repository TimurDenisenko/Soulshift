
namespace Assets.Scripts
{
    public interface IEntity
    {
        float MaxHealth { get; }
        float CurrentHealth { get; }
        float Defense { get; }
        float AttackPower { get; }
    }
}
