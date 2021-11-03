using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Inventory
{
    public class InventoryView : IInventoryView
    {
        public event EventHandler<IItem> Selected;
        public event EventHandler<IItem> Deselected;

        public void Display(IReadOnlyList<IItem> items)
        {
            //foreach (var item in items)
                //Debug.Log($"Id Item: {item.Id}. Title Item: {item.Info.Title}");
        }

      
        protected virtual void OnSelected(IItem e)
        {
            Selected?.Invoke(this, e);
        }

        protected virtual void OnDeselected(IItem e)
        {
            Deselected?.Invoke(this, e);
        }

    }
}
