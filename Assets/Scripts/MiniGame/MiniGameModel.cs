public class MiniGameModel
{

    private Enemy _enemy;

    private Money _money;
    private Health _health;
    private Power _power;
    private KnifeSkill _knifeSkill;
    private PistolSkill _pistolSkill;
    private PlayerWeaponRegime _weaponRegime;
    private CrimeLevel _crimeLevel;

    public Enemy Enemy => _enemy;
    public Money Money => _money;
    public Health Health => _health;
    public Power Power => _power;
    public KnifeSkill KnifeSkill => _knifeSkill;
    public PistolSkill PistolSkill => _pistolSkill;
    public PlayerWeaponRegime WeaponRegime => _weaponRegime;

    public CrimeLevel CrimeLevel => _crimeLevel;

   public MiniGameModel(Enemy enemy)
    {
        _enemy = enemy;
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
        _crimeLevel = new CrimeLevel(nameof(CrimeLevel));
    }



    public void OnDispose()
    {
        _money.Detach(_enemy);
        _health.Detach(_enemy);
        _power.Detach(_enemy);
        _knifeSkill.Detach(_enemy);
        _pistolSkill.Detach(_enemy);
        _weaponRegime.Detach(_enemy);
    }
}


