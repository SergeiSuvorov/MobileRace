using Inventory;
using Model;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameController : BaseController
{
    private  List<AsyncOperationHandle<GameObject>> _addressablePrefabs = new List<AsyncOperationHandle<GameObject>>();

    private ProfilePlayer _profilePlayer;
    private IInventoryModel _inventoryModel;
    private List<AbilityConfig> _abilitiesConfig;
    private Transform _placeForUi;
    public GameController(ProfilePlayer profilePlayer, IInventoryModel inventoryModel,
    List<AbilityConfig> abilitiesConfig, Transform placeForUi)
    {
        _abilitiesConfig = abilitiesConfig;
        _inventoryModel = inventoryModel;
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;
        LoadViewAsync("Level1");
    }
    private void CreateController(CarView carView, TapeBackgroundView tapeBackgroundView)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();
        var abilitiesRepository = new AbilitiesRepository(_abilitiesConfig);

        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff, tapeBackgroundView);
        AddController(tapeBackgroundController);

        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, _profilePlayer.CurrentCar);
        AddController(inputGameController);

        var carController = new CarController(_profilePlayer.CurrentCar, rightMoveDiff,carView);
        AddController(carController);

        var abilityViews = new ButtonsAbilityController(_placeForUi);

        var abilitiesController = new AbilitiesController(
            _inventoryModel, abilitiesRepository,
           abilityViews, carController);
        Debug.Log($"Game: car has speed : {_profilePlayer.CurrentCar.Speed}");
    }

    private async Task LoadViewAsync(string label)
    {
        CarView carView = null;
        TapeBackgroundView tapeBackgroundView = null;

        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;

        foreach (var location in locations)
        {
            var adressablePrefab = Addressables.InstantiateAsync(location);
            await adressablePrefab.Task;
            _addressablePrefabs.Add(adressablePrefab);

            var gameObject = adressablePrefab.Result;
            AddGameObjects((gameObject));


            if (carView==null)
            {
                Debug.Log(gameObject.TryGetComponent<CarView>(out carView));
            }
            if (tapeBackgroundView == null)
            {
                Debug.Log(gameObject.TryGetComponent<TapeBackgroundView>(out tapeBackgroundView));
            }
            if (carView == null && tapeBackgroundView == null)
            {
                Debug.LogWarning("Object haven't view " + gameObject.name);
            }
        }

        CreateController(carView, tapeBackgroundView);
    }
}

