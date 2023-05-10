using Inventory;
using System.Collections.Generic;
using Tools;

public class AbilitiesRepository : BaseController, IAbilityRepository
{
    public IReadOnlyDictionary<int, IAbility> AbilityMapById => _AbilityMapById;

    private Dictionary<int, IAbility> _AbilityMapById = new Dictionary<int, IAbility>();

    private readonly SubscriptionProperty<float> _leftMove;
    private readonly SubscriptionProperty<float> _rightMove;
    public AbilitiesRepository(List<AbilityConfig> abilityConfigs, SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove)
    {
        _leftMove = leftMove;
        _rightMove = rightMove;
        PopulateAbility(ref _AbilityMapById, abilityConfigs);
    }

    protected override void OnDispose()
    {
        _AbilityMapById.Clear();
        _AbilityMapById = null;
    }

    private void PopulateAbility(ref Dictionary<int, IAbility> abilityHandlersMapByType, List<AbilityConfig> configs)
    {
        foreach (var config in configs)
        {
            if (abilityHandlersMapByType.ContainsKey(config.Id))
                continue;

            abilityHandlersMapByType.Add(config.Id, CreateAbilityByType(config));
        }
    }

    private IAbility CreateAbilityByType(AbilityConfig ability)
    {
        switch (ability.Type)
        {
            case AbilityType.None:
                return new StubAbility();
            case AbilityType.Gun:
                return new GunAbility(ability.Power, ability.View);
            case AbilityType.Trap:
                return new TrapAbility(ability.Power, ability.View);
            case AbilityType.SpeedBonus:
                return new SpeedAbility(ability.Power, _rightMove);
            default:
                return new StubAbility();
        }
    }

}
