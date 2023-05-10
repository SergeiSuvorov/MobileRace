
using Garage;
using Inventory;
using Items;
using Model;
using Model.Shop;
using System.Collections.Generic;
using Tools.Ads;
using Ui;
using UnityEngine;

public class MainController : BaseController
{
    public MainController(Transform placeForUi, ProfilePlayer profilePlayer,
       List<ItemConfig> itemsConfig,
       List<AbilityConfig> abilitiesConfig,
       List<UpgradeItemConfig> upgradeItemsConfig,
       IAdsShower adsShower,
       List<ShopProduct> shopProducts,
       IShop shop)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        _itemsConfig = itemsConfig;
        _abilitiesConfig = abilitiesConfig;
        _upgradeItemsConfig = upgradeItemsConfig;
        _inventoryModel = new InventoryModel();
        _itemsRepository = new ItemsRepository(itemsConfig);
        _inventoryController = new InventoryController(_inventoryModel, _itemsRepository);
        _currentController = _inventoryController;
        _inventoryController.ShowInventory();
        AddController(_inventoryController);

        _adsShower = adsShower;
        _shop = shop;
        _purchaseController = new PurchaseController(shopProducts, profilePlayer, _shop);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly IAdsShower _adsShower;
    private readonly IShop _shop;

    private InventoryController _inventoryController;

    private readonly List<ItemConfig> _itemsConfig;
    private readonly List<AbilityConfig> _abilitiesConfig;
    private readonly List<UpgradeItemConfig> _upgradeItemsConfig;

    private InventoryModel _inventoryModel;
    private ItemsRepository _itemsRepository;
    private PurchaseController _purchaseController;
    private BaseController _currentController;
    protected override void OnDispose()
    {
        DisposeController();
        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        
        base.OnDispose();
    }

    private void DisposeController()
    {
        _currentController?.Dispose();
        _purchaseController?.Dispose();
        _inventoryController?.Dispose();
    }
    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.None:
                DisposeController();
                break;
            case GameState.Start:
                _currentController?.Dispose();
                _currentController = new MainMenuController(_placeForUi, _profilePlayer, _adsShower, _shop);
                break;
            case GameState.Game:
                _currentController?.Dispose();
                _profilePlayer.Analytic.SendMessage("Game Start", new Dictionary<string, object>());
                _currentController = new GameController(_profilePlayer, _inventoryModel, _abilitiesConfig, _placeForUi);
                break;
            case GameState.Garage:
                _currentController = new GarageController(_upgradeItemsConfig, _profilePlayer, _placeForUi, _currentController.Dispose);
                break;
            case GameState.Reward:
                _currentController = new DailyRewardController(_placeForUi, _profilePlayer, _currentController.Dispose);
                break;
            case GameState.MiniGame:
                _currentController?.Dispose();
                _currentController = new FightController(_placeForUi, _profilePlayer.CurrentState);
                break;
            default:
                DisposeController();
                break;
        }
    }
}
