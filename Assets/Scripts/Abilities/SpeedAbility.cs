using Tools;

public class SpeedAbility : IAbility
{
    private readonly float _power;

    public SpeedAbility(float power)
    {
        _power = power;
    }

    public void Apply(IAbilityActivator activator)
    {
        activator.ActivateAbility(this, _power);
    }
}
