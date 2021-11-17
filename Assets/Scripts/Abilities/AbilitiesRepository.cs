using Inventory;
using System.Collections.Generic;

public class AbilitiesRepository :BaseRepositoty<int, IAbility, AbilityConfig>,IAbilityRepository
{
    public AbilitiesRepository(List<AbilityConfig> configs) : base(configs)
    {
    }

    protected override IAbility CreateValue(AbilityConfig config)
    {
        switch (config.Type)
        {
            case AbilityType.None:
                return new StubAbility();
            case AbilityType.Gun:
                return new GunAbility(config.Power, config.View);
            case AbilityType.Trap:
                return new TrapAbility(config.Power, config.View);
            case AbilityType.SpeedBonus:
                return new SpeedAbility(config.Power);
            default:
                return new StubAbility();
        }
    }

}
