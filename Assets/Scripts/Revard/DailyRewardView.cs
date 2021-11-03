using System;
using System.Collections.Generic;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardView : MonoBehaviour
{
    private const string CurrentSlotInActiveKey = nameof(CurrentSlotInActiveKey);
    private const string TimeGetRewardKey = nameof(TimeGetRewardKey);

    [SerializeField]
    private float _timeCooldown = 7/*86400*/;

    [SerializeField]
    private float _timeDeadline = 14/*172800*/;

    [SerializeField]
    private List<Reward> _rewards;

    [SerializeField]
    private TMP_Text _timerNewReward;

    [SerializeField]
    private Transform _mountRootSlotsReward;

    [SerializeField]
    private ContainerSlotRewardView _containerSlotRewardView;

    [SerializeField]
    private Button _getRewardButton;

    [SerializeField]
    private Button _resetButton;

    [SerializeField]
    private Image _progressBarImage;

    public ContainerSlotRewardView ContainerSlotRewardView => _containerSlotRewardView;

    public Button GetRewardButton => _getRewardButton;

    public Button ResetButton => _resetButton;

    public Transform MountRootSlotsReward => _mountRootSlotsReward;

    public TMP_Text TimerNewReward => _timerNewReward;

    public List<Reward> Rewards => _rewards;

    public float TimeDeadline => _timeDeadline;

    public float TimeCooldown => _timeCooldown;

    public Image ProgressBarImage => _progressBarImage;

    [SerializeField]
    private TMP_Text _currentCountCredit;

    private SubscriptionProperty<int> _credit;

    public int CurrentSlotInActive
    {
        get => PlayerPrefs.GetInt(CurrentSlotInActiveKey, 0);
        set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
    }

    public DateTime? TimeGetReward
    {
        get
        {
            var data = PlayerPrefs.GetString(TimeGetRewardKey, null);

            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
            else
                PlayerPrefs.DeleteKey(TimeGetRewardKey);
        }
    }
    public void Init(SubscriptionProperty<int> credit)
    {
        _credit = credit;
        _credit.SubscribeOnChange(SetCurrentCreditCountText);
    }
    private void SetCurrentCreditCountText(int credit)
    {
        _currentCountCredit.text = credit.ToString();
    }

    private void OnDestroy()
    {
        _getRewardButton.onClick.RemoveAllListeners();
        _resetButton.onClick.RemoveAllListeners();
        _credit?.UnSubscriptionOnChange(SetCurrentCreditCountText);
    }
}
