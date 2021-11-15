using Items;
using System;
using System.Collections.Generic;

namespace Inventory
{
    public interface IInventoryController
    {
        IReadOnlyList<IItem> GetEquippedItems();
        void ShowInventory(Action hideAction);
        void HideInventory();

    }
}

