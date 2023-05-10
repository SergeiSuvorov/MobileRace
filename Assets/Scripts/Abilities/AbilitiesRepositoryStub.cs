using System.Collections.Generic;

public class AbilitiesRepositoryStub: IAbilityRepository
{
    public IReadOnlyDictionary<int, IAbility> AbilityMapById { get; } = new Dictionary<int, IAbility>();
}