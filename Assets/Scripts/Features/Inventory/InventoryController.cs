using System;
using System.Collections.Generic;
using System.Linq;
using Items;

namespace Inventory
{
    public class InventoryController : BaseController, IInventoryController
    {
        private readonly IInventoryModel _inventoryModel;
        private readonly IItemsRepository _itemsRepository;
        private readonly IInventoryView _inventoryView;
        private Action _hideAction;

        public InventoryController(IInventoryModel inventoryModel, IItemsRepository itemsRepository)
        {
            _inventoryModel = inventoryModel;
            _itemsRepository = itemsRepository;
            _inventoryView = new InventoryView();
        }

         

        public void ShowInventory()
        {
            foreach (var item in _itemsRepository.Content.Values)
                _inventoryModel.EquipItem(item);

            var equippedItems = _inventoryModel.GetEquippedItems();
            _inventoryView.Display(equippedItems);
        }

       

        public IReadOnlyList<IItem> GetEquippedItems()
        {
            return _inventoryModel.GetEquippedItems();
        }

        public void ShowInventory(Action hideAction)
        {
            _hideAction = hideAction;
            _inventoryView.Display(_itemsRepository.Content.Values.ToList());
        }

        public void HideInventory()
        {
            _hideAction?.Invoke();
        }

        private void SetupView(IInventoryView inventoryView)
        {
            inventoryView.Selected += OnItemSelected;
            inventoryView.Deselected += OnItemDeselected;
        }

        private void CleanupView()
        {
            _inventoryView.Selected -= OnItemSelected;
            _inventoryView.Deselected -= OnItemDeselected;
        }

        private void OnItemSelected(object sender, IItem item)
        {
            _inventoryModel.EquipItem(item);
        }

        private void OnItemDeselected(object sender, IItem item)
        {
            _inventoryModel.UnEquipItem(item);
        }



    }
}
