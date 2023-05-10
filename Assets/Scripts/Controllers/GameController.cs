using Inventory;
using Model;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class GameController : BaseController
{
    public GameController(ProfilePlayer profilePlayer, IInventoryModel inventoryModel,
    List<AbilityConfig> abilitiesConfig, Transform placeForUi)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();
        var abilitiesRepository = new AbilitiesRepository(abilitiesConfig);

        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);

        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
        AddController(inputGameController);

        var carController = new CarController(profilePlayer.CurrentCar);
        AddController(carController);

        var abilityViews = new ButtonsAbilityController(placeForUi);

        var abilitiesController = new AbilitiesController(
            inventoryModel, abilitiesRepository,
           abilityViews, carController);
        Debug.Log($"Game: car has speed : {profilePlayer.CurrentCar.Speed}");
    }
}

