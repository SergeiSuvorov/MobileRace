using Garage;
using JoostenProductions;
using Tools;
using UnityEngine;


public class CarController : BaseController, IAbilityActivator
{
    private  CarView _carView;
    private readonly IUpgradableCar _car;
    private readonly float _abilityBonusTime = 5f;
    private float _cyrentAbilityBonusTime=0;
    public CarController(IUpgradableCar car, SubscriptionProperty<float> moveDiff, CarView carView)
    {
        _car = car;
        _carView = carView;
        _carView.Init(moveDiff, _car.Speed.Value);

        Debug.Log(_carView == null);
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

