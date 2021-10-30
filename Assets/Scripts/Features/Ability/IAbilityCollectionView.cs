using System;
using System.Collections.Generic;
using Items;

public interface IAbilityCollectionView
{
    event EventHandler<IItem> UseRequested; 

    void Display(IReadOnlyList<IItem> abilityItems);

}