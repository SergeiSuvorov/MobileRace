using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbilityRepository
{
    IReadOnlyDictionary<int, IAbility> AbilityMapById { get; }
}
