using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace Items
{
    public class ItemsRepository : BaseRepositoty<int, IItem, ItemConfig>, IItemsRepository
    {
        public ItemsRepository(List<ItemConfig> configs) : base(configs)
        {

        }

        protected override IItem CreateValue(ItemConfig config)
        {
            return new Item
            {
                Id = config.Id,
                Info = new ItemInfo { Title = config.Title }
            };
        }
    }
}

