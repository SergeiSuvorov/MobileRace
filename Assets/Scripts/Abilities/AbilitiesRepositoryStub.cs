using System.Collections.Generic;

public class AbilitiesRepositoryStub: IAbilityRepository
{
    public IReadOnlyDictionary<int, IAbility> Content { get; } = new Dictionary<int, IAbility>();
}