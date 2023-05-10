using Garage;
using JoostenProductions;
using Tools;
using UnityEngine;

public class CarController : BaseController, IAbilityActivator
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/Car"};
    private readonly CarView _carView;
    private readonly IUpgradableCar _car;
    private readonly float _abilityBonusTime = 5f;
    private float _cyrentAbilityBonusTime=0;
    public CarController(IUpgradableCar car)
    {
        _car = car;
        _carView = LoadView();
    }

    private void AbilityTimer()
    {
        if (_cyrentAbilityBonusTime >= 0)
        {
            _cyrentAbilityBonusTime -= Time.deltaTime;
        }
        else
        {
            _car.Restore();
            UpdateManager.UnsubscribeFromUpdate(AbilityTimer);
        }
    }

    private CarView LoadView()
    {
        var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
        AddGameObjects(objView);
        
        return objView.GetComponent<CarView>();
    }

    public GameObject GetViewObject()
    {
        return _carView.gameObject;
    }

    public void ActivateAbility(IAbility ability, float power)
    {
       if(ability is SpeedAbility)
       {
            _car.SetSpeedBonus(power);
            _cyrentAbilityBonusTime = _abilityBonusTime;
            UpdateManager.SubscribeToUpdate(AbilityTimer);
        }  
    }
}

