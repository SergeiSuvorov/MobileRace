
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
        Debug.Log(itemsConfig == null);
        _itemsRepository = new ItemsRepository(itemsConfig);
        _inventoryController = new InventoryController(_inventoryModel, _itemsRepository);
        _inventoryController.ShowInventory();
        AddController(_inventoryController);
        
        _adsShower = adsShower;
        _shop = shop;
        _purchaseController = new PurchaseController(shopProducts, profilePlayer, _shop);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);        
    }

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private PurchaseController _purchaseController;
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
    private GarageController _garageController;
    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _garageController?.Dispose();
        _purchaseController?.Dispose();
        _inventoryController?.Dispose();
        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.None:
                break;
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _adsShower, _shop);
                _garageController?.Dispose();
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _profilePlayer.Analytic.SendMessage("Game Start", new Dictionary<string, object>());
                _garageController?.Dispose();
                _mainMenuController?.Dispose();
                _gameController = new GameController(_profilePlayer, _inventoryModel,_abilitiesConfig, _placeForUi);
                break;
            case GameState.Garage:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                _garageController = new GarageController(_upgradeItemsConfig, _profilePlayer, _placeForUi);
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                _garageController?.Dispose();
                break;
        } 
    }
}
