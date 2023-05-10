using Model;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.Events;

public class FightController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/MiniGameWindow" };
    private readonly FightWindowView _view;
    private readonly Transform _placeForUi;
    private readonly SubscriptionProperty<GameState> _gameState;

    private MiniGameModel _model;
    public FightController(Transform placeForUi, SubscriptionProperty<GameState> gameState)
    {
        _gameState = gameState;

        var enemy = new Enemy("Flappy");
        _model = new MiniGameModel(enemy);

        _placeForUi = placeForUi;
        _view = LoadView(_placeForUi);
        _view.Init();
        _view.AddMoneyButton.onClick.AddListener(() => ChangeData(DataType.Money,true));
        _view.MinusMoneyButton.onClick.AddListener(() => ChangeData(DataType.Money, false));
        _view.AddHealthButton.onClick.AddListener(() => ChangeData(DataType.Health, true));
        _view.MinusHealthButton.onClick.AddListener(() => ChangeData(DataType.Health, false));
        _view.AddPowerButton.onClick.AddListener(() => ChangeData(DataType.Power, true));
        _view.MinusPowerButton.onClick.AddListener(() => ChangeData(DataType.Power, false));
        _view.AddCrimeButton.onClick.AddListener(() => ChangeData(DataType.CrimeLevel, true));
        _view.MinusCrimeButton.onClick.AddListener(() => ChangeData(DataType.CrimeLevel, false));
        _view.AddCrimeButton.onClick.AddListener(CheckPassPeacefullyRegime);
        _view.MinusCrimeButton.onClick.AddListener(CheckPassPeacefullyRegime);
        _view.PassPeacefullyButton.onClick.AddListener(PassPeacefully);
        _view.PistolWeaponButton.onClick.AddListener(() => ChangeWeaponRegime(WeaponRegime.Pistol));
        _view.KnifeWeaponButton.onClick.AddListener(() => ChangeWeaponRegime(WeaponRegime.Knife));
        _view.FightButton.onClick.AddListener(Fight);
        _view.ExitButton.onClick.AddListener(BackToMainMenu);
        _view.UpdateGameInfo(_model.Money.CountMoney, _model.Health.CountHealth, _model.Power.CountPower, _model.KnifeSkill.CountKnifeSkill, _model.PistolSkill.CountPistolSkill,
            0, _model.CrimeLevel.CountCrimeLevel, _model.Enemy.Power);
    }

    private void BackToMainMenu()
    {
        _gameState.Value = GameState.Start;
    }
    private void ChangeData(DataType dataType, bool isAddCount)
    {
        int changeCount = 0;
        if (isAddCount)
            changeCount++;
        else
            changeCount--;

        ChangeDataWindow(changeCount, dataType);
    }
    private FightWindowView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);

        return objectView.GetComponent<FightWindowView>();
    }
    protected override void OnDispose()
    {
        _model.OnDispose();
    }

    private void Fight()
    {
        Debug.Log((CalculateEffectivePlayerPower() >= _model.Enemy.Power ? "Win" : "Lose") + " with " + _model.WeaponRegime.WeaponRegime);
    }

    private void PassPeacefully()
    {
        Debug.Log("PassPeacefully");
    }

    private void ChangeWeaponRegime(WeaponRegime weaponRegime)
    {
        _model.WeaponRegime.WeaponRegime = weaponRegime;
    }

    private void CheckPassPeacefullyRegime()
    {
       bool canPassPacefull = true;
       if (_model.CrimeLevel.CountCrimeLevel >= 3)
            canPassPacefull = false;
        _view.PassPeacefullyButton.gameObject.SetActive(canPassPacefull);

    }

    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                if (_model.WeaponRegime.WeaponRegime == WeaponRegime.Pistol)
                {
                    _model.PistolSkill.CountPistolSkill += countChangeData;
                }
                _model.Money.CountMoney += countChangeData;
                break;

            case DataType.Health:
                _model.Health.CountHealth += countChangeData;
                break;

            case DataType.Power:
                if (_model.WeaponRegime.WeaponRegime == WeaponRegime.Knife)
                {
                    _model.KnifeSkill.CountKnifeSkill += countChangeData;
                }
                _model.Power.CountPower += countChangeData;
                break;
            case DataType.CrimeLevel:
                _model.CrimeLevel.CountCrimeLevel += countChangeData;
                break;
        }
        var playerAtack = CalculateEffectivePlayerPower();

        _view.UpdateGameInfo(_model.Money.CountMoney, _model.Health.CountHealth, _model.Power.CountPower, _model.KnifeSkill.CountKnifeSkill, _model.PistolSkill.CountPistolSkill,
            playerAtack, _model.CrimeLevel.CountCrimeLevel, _model.Enemy.Power);
    }

    private int CalculateEffectivePlayerPower()
    {
        int effectivePowerlPlayer = 0;
        switch (_model.WeaponRegime.WeaponRegime)
        {
            case WeaponRegime.None:
                effectivePowerlPlayer = _model.Power.CountPower;
                break;
            case WeaponRegime.Knife:
                effectivePowerlPlayer = _model.Power.CountPower + _model.KnifeSkill.CountKnifeSkill;
                break;
            case WeaponRegime.Pistol:
                effectivePowerlPlayer = _model.PistolSkill.CountPistolSkill;
                break;
        }

        return effectivePowerlPlayer;
    }
}


