using System.Collections.Generic;

public class DataPlayer
{
    private string _titleData;

    private int _countMoney;
    private int _countHealth;
    private int _countPower;

    private int _countPistolSkill;
    private int _countKnifeSkill;

    private WeaponRegime _weaponRegime;

    private List<IEnemy> _enemies = new List<IEnemy>();

    public DataPlayer(string titleData)
    {
        _titleData = titleData;
    }

    public string TitleData => _titleData;

    public int CountMoney 
    { 
        get => _countMoney;
        set
        {
            if (_countMoney != value)
            {
                _countMoney = value;
                Notifier(DataType.Money);
            }
        }
    }

    public int CountHealth
    {
        get => _countHealth;
        set
        {
            if (_countHealth != value)
            {
                _countHealth = value;
                Notifier(DataType.Health);
            }
        }
    }

    public int CountPower
    {
        get => _countPower;
        set
        {
            if (_countPower != value)
            {
                _countPower = value;
                Notifier(DataType.Power);
            }
        }
    }

    public int CountPistolSkill
    {
        get => _countPistolSkill;
        set
        {
            if (_countPistolSkill != value)
            {
                _countPistolSkill = value;
                Notifier(DataType.PistolSkill);
            }
        }
    }

    public int CountKnifeSkill
    {
        get => _countKnifeSkill;
        set
        {
            if (_countKnifeSkill != value)
            {
                _countKnifeSkill = value;
                Notifier(DataType.KnifeSkill);
            }
        }
    }

    public WeaponRegime WeaponRegime
    {
        get => _weaponRegime;
        set
        {
            if(_weaponRegime!=value)
            {
                _weaponRegime = value;
                Notifier(value);
            }
        }
    }

    public void Attach(IEnemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void Detach(IEnemy enemy)
    {
        _enemies.Remove(enemy);
    }

    private void Notifier(DataType dataType)
    {
        foreach(var enemy in _enemies)
            enemy.Update(this, dataType);
    }

    private void Notifier(WeaponRegime weaponType)
    {
        foreach (var enemy in _enemies)
            enemy.Update(this, weaponType);
    }
}

public class Money : DataPlayer
{
    public Money(string titleData) : base(titleData)
    {
    }
}

public class Health : DataPlayer
{
    public Health(string titleData) : base(titleData)
    {
    }
}

public class Power : DataPlayer
{
    public Power(string titleData) : base(titleData)
    {
    }
}

public class KnifeSkill : DataPlayer
{
    public KnifeSkill(string titleData) : base(titleData)
    {
    }
}

public class PistolSkill : DataPlayer
{
    public PistolSkill(string titleData) : base(titleData)
    {
    }
}

public class PlayerWeaponRegime : DataPlayer
{
    public PlayerWeaponRegime(string titleData) : base(titleData)
    {
    }
}
