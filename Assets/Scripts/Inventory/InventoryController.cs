using System.Collections.Generic;
using Items;

namespace Inventory
{
    public class InventoryController : BaseController, IInventoryController
    {
        private readonly IInventoryModel _inventoryModel;
        private readonly IItemsRepository _itemsRepository;
        private readonly IInventoryView _inventoryWindowView;
        
        public InventoryController(IInventoryModel inventoryModel, IItemsRepository itemsRepository)
        {
            _inventoryModel = inventoryModel;
            _itemsRepository = itemsRepository;
            _inventoryWindowView = new InventoryView();
        }

        public void ShowInventory()
        {
            foreach (var item in _itemsRepository.Items.Values)
                _inventoryModel.EquipItem(item);

            var equippedItems = _inventoryModel.GetEquippedItems();
            _inventoryWindowView.Display(equippedItems);
        }
    }
}
