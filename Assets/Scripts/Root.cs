
using Model;
using Model.Analytic;
using Model.Shop;
using System.Collections.Generic;
using Tools.Ads;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] 
    private Transform _placeForUi;
    [SerializeField]
    private UnityAdsTools _unityAdsTools;
    private MainController _mainController;
    [SerializeField]
    private List<ShopProduct> _shopProducts;

    private void Awake()
    {
        var analytic = new UnityAnalyticTools();
        var profilePlayer = new ProfilePlayer(15f, analytic);
        profilePlayer.CurrentState.Value = GameState.Start;
        var shop = new ShopTools(_shopProducts);
        _mainController = new MainController(_placeForUi, profilePlayer, _unityAdsTools, _shopProducts,shop);
        analytic.SendMessage("Game Start", new Dictionary<string, object>());
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
