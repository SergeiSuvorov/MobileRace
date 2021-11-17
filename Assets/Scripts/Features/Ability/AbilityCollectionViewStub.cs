using System;
using System.Collections.Generic;
using Items;

public class AbilityCollectionViewStub : IAbilityCollectionView
{
    public event EventHandler<IItem> UseRequested;
    public void Display(IReadOnlyList<IItem> abilityItems)
    {
        foreach (var item in abilityItems)
        {
            UnityEngine.Debug.Log($"AbilityItem: {item.Id} {item.Info.Title}");
        }
    }
}