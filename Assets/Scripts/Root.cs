
using Inventory;
using Model;
using Model.Analytic;
using Model.Shop;
using System.Collections.Generic;
using System.Linq;
using Tools.Ads;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] 
    private Transform _placeForUi;

    [SerializeField]
    private UnityAdsTools _unityAdsTools;

    [SerializeField]
    private ItemConfig[] _itemsConfig;

    [SerializeField]
    private AbilityConfig[] _abilitiesConfig;

    [SerializeField]
    private UpgradeItemConfig[] _upgradeItemsConfig;

    [SerializeField]
    private List<ShopProduct> _shopProducts;

    private MainController _mainController;
    private void Awake()
    {
        var analytic = new UnityAnalyticTools();
        var profilePlayer = new ProfilePlayer(15f, analytic);
        profilePlayer.CurrentState.Value = GameState.Start;
        var shop = new ShopTools(_shopProducts);
        //_mainController = new MainController(_placeForUi, profilePlayer, _unityAdsTools, _shopProducts,shop);
        _mainController = new MainController(_placeForUi, profilePlayer, _itemsConfig.ToList(),
            _abilitiesConfig.ToList(), _upgradeItemsConfig.ToList(), _unityAdsTools, _shopProducts, shop);
        analytic.SendMessage("Game Start", new Dictionary<string, object>());
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
