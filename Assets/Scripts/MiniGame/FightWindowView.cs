using TMPro;
using UnityEngine;
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
    [SerializeField]
    private Button _exitButton;


    public Button AddMoneyButton => _addMoneyButton;
    public Button MinusMoneyButton => _minusMoneyButton;
    public Button AddHealthButton => _addHealthButton;
    public Button MinusHealthButton => _minusHealthButton;
    public Button AddPowerButton => _addPowerButton;
    public Button MinusPowerButton => _minusPowerButton;
    public Button AddCrimeButton => _addCrimeButton;
    public Button MinusCrimeButton => _minusCrimeButton;
    public Button FightButton => _fightButton;
    public Button PassPeacefullyButton => _passPeacefullyButton;
    public Button PistolWeaponButton => _pistolWeaponButton;
    public Button KnifeWeaponButton => _knifeWeaponButton;
    public Button ExitButton => _exitButton;

    public void Init()
    {
        
    }

    private void OnDestroy()
    {
        Debug.Log(1234567890);
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
        _exitButton.onClick.RemoveAllListeners();
    }

  
    public void UpdateGameInfo(int playerMoney, int playerHealth, int playerPower, int playerKnifeSkill, 
        int playerPistolSkill, int playerAtack, int crimeLevel, int enemyPower)
    {
        _countMoneyText.text = $"Player Money: {playerMoney}";
        _countHealthText.text = $"Player Health: {playerHealth}";
        _countPowerText.text = $"Player Power: {playerPower}";
        _countKnifeSkillText.text = $"Knife Skill: {playerKnifeSkill}";
        _countPistolSkillText.text = $"Pistol Skill: { playerPistolSkill}";
        _countEffectivePlayerPowerText.text = $"Player Atack: {playerAtack}";
        _countCrimeLevelText.text = $"Crime Level: {crimeLevel}";
        _countPowerEnemyText.text = $"Enemy power: {enemyPower}";
    }
   
}

public enum WeaponRegime
{
    None,
    Knife,
    Pistol
}

