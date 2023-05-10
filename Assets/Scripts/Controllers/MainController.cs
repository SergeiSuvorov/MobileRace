
using Model;
using Model.Shop;
using System.Collections.Generic;
using Tools.Ads;
using Ui;
using UnityEngine;

public class MainController : BaseController
{
    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, IAdsShower adsShower, List<ShopProduct> shopProducts, IShop shop)
    {
        _adsShower = adsShower;
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
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

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _purchaseController?.Dispose();
        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _adsShower, _shop);
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _profilePlayer.Analytic.SendMessage("Game Start", new Dictionary<string, object>());
                _gameController = new GameController(_profilePlayer);
                _mainMenuController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
        }
    }
}
