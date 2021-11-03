using TMPro;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
    private const string WoodKey = nameof(WoodKey);
    private const string DiamondKey = nameof(DiamondKey);
    private const string GoldKey = nameof(GoldKey);

    [SerializeField]
    private TMP_Text _currentCountWood;

    [SerializeField]
    private TMP_Text _currentCountDiamond;

    [SerializeField]
    private TMP_Text _currentCountGold;

    public static CurrencyView Instance { get; private set; }

    private int Wood
    {
        get => PlayerPrefs.GetInt(WoodKey, 0);
        set => PlayerPrefs.SetInt(WoodKey, value);
    }

    private int Diamond
    {
        get => PlayerPrefs.GetInt(DiamondKey, 0);
        set => PlayerPrefs.SetInt(DiamondKey, value);
    }

    private int Gold
    {
        get => PlayerPrefs.GetInt(GoldKey, 0);
        set => PlayerPrefs.SetInt(GoldKey, value);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        RefreshText();
    }

    public void AddWood(int value)
    {
        Wood += value;

        RefreshText();
    }

    public void AddDiamond(int value)
    {
        Diamond += value;

        RefreshText();
    }

    public void AddGold(int value)
    {
        Gold += value;

        RefreshText();
    }

    private void RefreshText()
    {
        _currentCountWood.text = Wood.ToString();
        _currentCountDiamond.text = Diamond.ToString();
        _currentCountGold.text = Gold.ToString();
    }
}
