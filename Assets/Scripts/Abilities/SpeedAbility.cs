using Tools;

public class SpeedAbility : IAbility
{
    private readonly SubscriptionProperty<float> _rightMove;
    private readonly float _power;

    public SpeedAbility(float power, SubscriptionProperty<float> rightMove)
    {
        _power = power;
        _rightMove = rightMove;
    }

    public void Apply(IAbilityActivator activator)
    {
        _rightMove.Value += _power;
    }
}
