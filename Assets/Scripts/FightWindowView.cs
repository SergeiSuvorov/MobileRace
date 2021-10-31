using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FightWindowView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _countMoneyText;

    [SerializeField]
    private TMP_Text _countHealthText;

    [SerializeField]
    private TMP_Text _countPowerText;

    [SerializeField]
    private TMP_Text _countEffectivePlayerPowerText;

    [SerializeField]
    private TMP_Text _countCrimeLevelText;
    [SerializeField]
    private TMP_Text _countPowerEnemyText;
    [SerializeField]
    private TMP_Text _countKnifeSkillText;
    [SerializeField]
    private TMP_Text _countPistolSkillText;


    [SerializeField]
    private Button _addMoneyButton;

    [SerializeField]
    private Button _minusMoneyButton;


    [SerializeField]
    private Button _addHealthButton;

    [SerializeField]
    private Button _minusHealthButton;


    [SerializeField]
    private Button _addPowerButton;

    [SerializeField]
    private Button _minusPowerButton;

    [SerializeField]
    private Button _addCrimeButton;

    [SerializeField]
    private Button _minusCrimeButton;

    [SerializeField]
    private Button _fightButton;

    [SerializeField]
    private Button _passPeacefullyButton;
    [SerializeField]
    private Button _pistolWeaponButton;

    [SerializeField]
    private Button _knifeWeaponButton;



    private Enemy _enemy;

    private Money _money;
    private Health _health;
    private Power _power;
    private KnifeSkill _knifeSkill;
    private PistolSkill _pistolSkill;
    private PlayerWeaponRegime _weaponRegime;

    private int _allCountMoneyPlayer;
    private int _allCountHealthPlayer;
    private int _allCountPowerPlayer;
    private int _allCountKnifeSkillPlayer;
    private int _allCountPistolSkillPlayer;
    private int _allCountCrimeLevelPlayer;
    //private int _allCountEffectivePowerlPlayer;

    private void Start()
    {
        _enemy = new Enemy("Flappy");

        _money = new Money(nameof(Money));
        _money.Attach(_enemy);

        _health = new Health(nameof(Health));
        _health.Attach(_enemy);

        _power = new Power(nameof(Power));
        _power.Attach(_enemy);

        _knifeSkill = new KnifeSkill(nameof(KnifeSkill));
        _knifeSkill.Attach(_enemy);

        _pistolSkill = new PistolSkill(nameof(PistolSkill));
        _pistolSkill.Attach(_enemy);

        _weaponRegime = new PlayerWeaponRegime(nameof(PlayerWeaponRegime));
        _weaponRegime.Attach(_enemy);

        _addMoneyButton.onClick.AddListener(() => ChangeMoney(true));
        _minusMoneyButton.onClick.AddListener(() => ChangeMoney(false));

        _addHealthButton.onClick.AddListener(() => ChangeHealth(true));
        _minusHealthButton.onClick.AddListener(() => ChangeHealth(false));

        _addPowerButton.onClick.AddListener(() => ChangePower(true));
        _minusPowerButton.onClick.AddListener(() => ChangePower(false));

        _addCrimeButton.onClick.AddListener(() => ChangeCrimeLevel(true));
        _minusCrimeButton.onClick.AddListener(() => ChangeCrimeLevel(false));

        _passPeacefullyButton.onClick.AddListener(PassPeacefully);

        _pistolWeaponButton.onClick.AddListener(() => ChangeWeaponRegime(WeaponRegime.Pistol));
        _knifeWeaponButton.onClick.AddListener(() => ChangeWeaponRegime(WeaponRegime.Knife));

        _fightButton.onClick.AddListener(Fight);
    }

    private void OnDestroy()
    {
        _addMoneyButton.onClick.RemoveAllListeners();
        _minusMoneyButton.onClick.RemoveAllListeners();

        _addHealthButton.onClick.RemoveAllListeners();
        _minusHealthButton.onClick.RemoveAllListeners();

        _addPowerButton.onClick.RemoveAllListeners();
        _minusPowerButton.onClick.RemoveAllListeners();

        _addCrimeButton.onClick.RemoveAllListeners();
        _minusCrimeButton.onClick.RemoveAllListeners();

        _pistolWeaponButton.onClick.RemoveAllListeners();
        _knifeWeaponButton.onClick.RemoveAllListeners();

        _passPeacefullyButton.onClick.RemoveAllListeners();

        _fightButton.onClick.RemoveAllListeners();

        _money.Detach(_enemy);
        _health.Detach(_enemy);
        _power.Detach(_enemy);
    }

    private void Fight()
    {
        Debug.Log((CalculateEffectivePlayerPower() >= _enemy.Power ? "Win" : "Lose" ) + " with " + _weaponRegime.WeaponRegime);
    }

    private void PassPeacefully()
    {
        Debug.Log("PassPeacefully"); 
    }

    private void ChangePower(bool isAddCount)
    {
        if (isAddCount)
            _allCountPowerPlayer++;
        else
            _allCountPowerPlayer--;

        ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
    }

    private void ChangeHealth(bool isAddCount)
    {
        if (isAddCount)
            _allCountHealthPlayer++;
        else
            _allCountHealthPlayer--;

        ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
    }

    private void ChangeWeaponRegime(WeaponRegime weaponRegime)
    {
        _weaponRegime.WeaponRegime = weaponRegime;
    }

    private void ChangeMoney(bool isAddCount)
    {
        if (isAddCount)
            _allCountMoneyPlayer++;
        else
            _allCountMoneyPlayer--;

        ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
    }

    private void ChangeCrimeLevel(bool isAddCount)
    {
        if (isAddCount)
            _allCountCrimeLevelPlayer++;
        else
            _allCountCrimeLevelPlayer--;
        CheckPassPeacefullyRegime();
        ChangeDataWindow(_allCountCrimeLevelPlayer, DataType.CrimeLevel);
    }

    private void CheckPassPeacefullyRegime()
    {
        if (_allCountCrimeLevelPlayer < 3 && !_passPeacefullyButton.gameObject.active)
            _passPeacefullyButton.gameObject.SetActive(true);
        else if(_allCountCrimeLevelPlayer >= 3 && _passPeacefullyButton.gameObject.active)
            _passPeacefullyButton.gameObject.SetActive(false);

    }
   
    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                if (_weaponRegime.WeaponRegime == WeaponRegime.Pistol)
                {
                    _allCountPistolSkillPlayer += countChangeData - _money.CountMoney;
                    _pistolSkill.CountPistolSkill = _allCountPistolSkillPlayer; 
                    _countPistolSkillText.text = $"Pistol Skill: {_pistolSkill.CountPistolSkill}";
                }
                _money.CountMoney = countChangeData;
                _countMoneyText.text = $"Player Money: {countChangeData}";
                break;

            case DataType.Health:
                _countHealthText.text = $"Player Health: {countChangeData}";
                _health.CountHealth = countChangeData;
                break;

            case DataType.Power:
                if (_weaponRegime.WeaponRegime == WeaponRegime.Knife)
                {
                    _allCountKnifeSkillPlayer += countChangeData - _power.CountPower;
                    _knifeSkill.CountKnifeSkill += _allCountKnifeSkillPlayer;
                    _countKnifeSkillText.text = $"Knife Skill: {_knifeSkill.CountKnifeSkill}";
                }
                _power.CountPower = countChangeData;
                _countPowerText.text = $"Player Power: {countChangeData}";
                break;
            case DataType.CrimeLevel:
                _countCrimeLevelText.text = $"Crime Level: {countChangeData}";
                break;
        }
        _countEffectivePlayerPowerText.text = $"Player Atack: {CalculateEffectivePlayerPower()}";

        _countPowerEnemyText.text = $"Enemy power: {_enemy.Power}";
    }

    private int CalculateEffectivePlayerPower()
    {
        int effectivePowerlPlayer = 0;
        switch (_weaponRegime.WeaponRegime)
        {
            case WeaponRegime.None:
                effectivePowerlPlayer = _allCountPowerPlayer;
                break;
            case WeaponRegime.Knife:
                effectivePowerlPlayer = _allCountPowerPlayer + _allCountKnifeSkillPlayer;
                break;
            case WeaponRegime.Pistol:
                effectivePowerlPlayer = _allCountPistolSkillPlayer;
                break;
        }

        return effectivePowerlPlayer;
    }
}

public enum WeaponRegime
{
    None,
    Knife,
    Pistol
}

