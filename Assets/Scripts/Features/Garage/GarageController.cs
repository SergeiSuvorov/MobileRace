using System.Collections.Generic;
using System.Linq;
using Inventory;
using Items;
using JetBrains.Annotations;
using Model;
using Tools;
using UnityEngine;
using UnityEngine.Events;

namespace Garage
{
    public class GarageController : BaseController, IShedController
    {
        private readonly Car _car;

        private readonly UpgradeHandlersRepository _upgradeHandlersRepository;
        private readonly ItemsRepository _upgradeItemsRepository;
        private readonly InventoryModel _inventoryModel;
        private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/GarageUI" };
        private readonly Transform _placeForUi;
        private readonly List<UpgradeItemConfig> _upgradeItemConfigs;
        private readonly ProfilePlayer _profilePlayer;  

        private GarageView _view;
        UnityAction _onPopUpShow;
        UnityAction _onPopUpHide;
        public GarageController([NotNull] List<UpgradeItemConfig> upgradeItemConfigs, [NotNull] ProfilePlayer profilePlayer, [NotNull] Transform placeForUi, UnityAction onPopUpShow)
        {
            _profilePlayer = profilePlayer;
            _car = _profilePlayer.CurrentCar;
            _placeForUi = placeForUi;
            _upgradeHandlersRepository
                = new UpgradeHandlersRepository(upgradeItemConfigs);
            AddController(_upgradeHandlersRepository);

            _upgradeItemsRepository
                = new ItemsRepository(upgradeItemConfigs.Select(value => value.ItemConfig).ToList());
            AddController(_upgradeItemsRepository);
        
            _inventoryModel = new InventoryModel();

            _upgradeItemConfigs = upgradeItemConfigs;

            _onPopUpShow = onPopUpShow;
            _onPopUpHide = this.Dispose;
            Enter();
        }
        private GarageView LoadView(Transform placeForUi)
        {
            Debug.Log(placeForUi);
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
            AddGameObjects(objectView);

            return objectView.GetComponent<GarageView>();
        }
        public void Enter()
        {
            _view = LoadView(_placeForUi);
            _view.Init(_upgradeItemConfigs,_onPopUpShow, _onPopUpHide);
            Debug.Log($"Enter: car has speed : {_car.Speed}");
            Debug.Log($"Enter: has detail in garage: {_upgradeItemsRepository.Content.Count}");
            foreach (var equippedItem in _upgradeItemsRepository.Content)
            {
                 Debug.Log(equippedItem.Value.Id + " "+ equippedItem.Value.Info.Title);
            }

            Debug.Log($"Enter: has ubgarade detail in garage: {_upgradeHandlersRepository.Content.Count}");
            for (int i = 1; i <= _upgradeHandlersRepository.Content.Count; i++)
            {
                Debug.Log(_upgradeHandlersRepository.Content[i].ToString());
            }

            _view.Apply = SetEquippedItems;
        }

        private void SetEquippedItems(List<UpgradeItemConfig> upgradeItemConfigs)
        {
            for (int i = 0; i < upgradeItemConfigs.Count; i++)
            {
                var id = upgradeItemConfigs[i].Id;
                _inventoryModel.EquipItem(_upgradeItemsRepository.Content[id]);
            }
            Exit();
        }
        public void Exit()
        {
            UpgradeCarWithEquippedItems(_car, _inventoryModel.GetEquippedItems(), _upgradeHandlersRepository.Content);
            Debug.Log($"Exit: car has speed : {_car.Speed}");
            _profilePlayer.CurrentState.Value = GameState.Start;
        }
    
        private void UpgradeCarWithEquippedItems(
            IUpgradableCar upgradableCar,
            IReadOnlyList<IItem> equippedItems,
            IReadOnlyDictionary<int, IUpgradeCarHandler> upgradeHandlers)
        {
            foreach (var equippedItem in equippedItems)
            {
                if (upgradeHandlers.TryGetValue(equippedItem.Id, out var handler))
                    handler.Upgrade(upgradableCar);
            }
        }
    }
}

