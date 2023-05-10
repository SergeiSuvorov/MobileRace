using System.Collections.Generic;
using Items;

namespace Inventory
{
    public interface IInventoryView
    {
        void Display(IReadOnlyList<IItem> items);
    }
}
