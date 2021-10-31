using UnityEngine;

public class Enemy : IEnemy
{
    private string _name;

    private int _moneyPlayer;
    private int _healthPlayer;
    private int _powerPlayer;
    private int _pistolSkillPlayer;
    private int _knifeSkillPlayer;
    private int _randomSummand=0;

    private WeaponRegime _weaponRegime;
    public Enemy(string name)
    {
        _name = name;
    }

    public void Update(DataPlayer dataPlayer, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Health:
                _healthPlayer = dataPlayer.CountHealth;
                break;

            case DataType.Money:
                _moneyPlayer = dataPlayer.CountMoney;
                break;

            case DataType.Power:
                _powerPlayer = dataPlayer.CountPower;
                break;
            case DataType.PistolSkill:
                _pistolSkillPlayer = dataPlayer.CountPistolSkill;
                break;
            case DataType.KnifeSkill:
                _knifeSkillPlayer = dataPlayer.CountKnifeSkill;
                break;
        }
        _randomSummand = Random.Range(-5, 5);

        Debug.Log($"Update {_name}, change {dataType}");
    }

    public void Update(DataPlayer dataPlayer, WeaponRegime weapon)
    {
        _weaponRegime = weapon;
        Debug.Log("Change weapon - " + weapon);
    }

    public int Power
    {
        get
        {
            int power=0;

            switch (_weaponRegime)
            {
                case WeaponRegime.None:
                    power = _moneyPlayer + _healthPlayer - _powerPlayer + _randomSummand;
                    break;
                case WeaponRegime.Knife:
                    power = 2*_moneyPlayer + _healthPlayer - _powerPlayer + _randomSummand-_knifeSkillPlayer;
                    break;
                case WeaponRegime.Pistol:
                    power = 3* _moneyPlayer + _healthPlayer  + _randomSummand - 3 *_pistolSkillPlayer;
                    break;
            }
            
            return power;
        }
    }
}
