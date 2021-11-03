using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class DailyRewardController: BaseController
{
    private DailyRewardView _dailyRewardView;
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/Rewards" };
    private List<ContainerSlotRewardView> _slots;
    private SubscriptionProperty<int> _credit;

    private bool _isGetReward;

    public DailyRewardController(DailyRewardView generateLevelView)
    {
        _dailyRewardView = generateLevelView;
    }
    public DailyRewardController(Transform placeForUI, SubscriptionProperty<int> credit)
    {
        _credit = credit;
        _dailyRewardView = LoadView(placeForUI);
        _dailyRewardView.Init(_credit);
        RefreshView();
    }
    private DailyRewardView LoadView(Transform placeForUi)
    {
        Debug.Log(placeForUi);
        var objectView = UnityEngine.Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);

        return objectView.GetComponentInChildren<DailyRewardView>();
    }
    public void RefreshView()
   {
       InitSlots();
      
       _dailyRewardView.StartCoroutine(RewardsStateUpdater());
      
       RefreshUi();
       SubscribeButtons();
   }

   private void InitSlots()
   {
       _slots = new List<ContainerSlotRewardView>();
       
        for (var i = 0; i < _dailyRewardView.Rewards.Count; i++)
       {
           var instanceSlot = GameObject.Instantiate(_dailyRewardView.ContainerSlotRewardView,
               _dailyRewardView.MountRootSlotsReward, false);

           _slots.Add(instanceSlot);
       }
   }

   private IEnumerator RewardsStateUpdater()
   {
       while (true)
       {
           RefreshRewardsState();
           yield return new WaitForSeconds(1);
       }
   }

   private void RefreshRewardsState()
   {
       _isGetReward = true;

       if (_dailyRewardView.TimeGetReward.HasValue)
       {
           var timeSpan = DateTime.UtcNow - _dailyRewardView.TimeGetReward.Value;

           if (timeSpan.Seconds > _dailyRewardView.TimeDeadline)
           {
               _dailyRewardView.TimeGetReward = null;
               _dailyRewardView.CurrentSlotInActive = 0;
           }
           else if (timeSpan.Seconds < _dailyRewardView.TimeCooldown)
           {
               _isGetReward = false;
           }
       }

       RefreshUi();
   }

   private void RefreshUi()
   {
       _dailyRewardView.GetRewardButton.interactable = _isGetReward;

       if (_isGetReward)
       {
           _dailyRewardView.TimerNewReward.text = "The reward is received today";
           _dailyRewardView.ProgressBarImage.fillAmount = 1f;
       }
       else
       {
           if (_dailyRewardView.TimeGetReward != null)
           {
               var nextClaimTime = _dailyRewardView.TimeGetReward.Value.AddSeconds(_dailyRewardView.TimeCooldown);
                Debug.Log(_dailyRewardView.TimeGetReward.Value);
                Debug.Log(nextClaimTime);
                Debug.Log(DateTime.UtcNow);

                var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
               var timeGetReward = $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";
      
               _dailyRewardView.TimerNewReward.text = $"Time to get the next reward: {timeGetReward}";
               _dailyRewardView.ProgressBarImage.fillAmount = (int)(_dailyRewardView.TimeCooldown - (float)currentClaimCooldown.TotalSeconds) / _dailyRewardView.TimeCooldown;
                Debug.Log(_dailyRewardView.TimeCooldown);
                Debug.Log((float)currentClaimCooldown.TotalSeconds);
                Debug.Log(_dailyRewardView.TimeCooldown - (float)currentClaimCooldown.TotalSeconds);
                Debug.Log("________________________________________________________________________");
            }
       }



       for (var i = 0; i < _slots.Count; i++)
           _slots[i].SetData(_dailyRewardView.Rewards[i],i + 1, i == _dailyRewardView.CurrentSlotInActive);
   }

   private void SubscribeButtons()
   {
       _dailyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
       _dailyRewardView.ResetButton.onClick.AddListener(ResetTimer);
   }

   private void ClaimReward()
   {
       if (!_isGetReward)
           return;

       var reward = _dailyRewardView.Rewards[_dailyRewardView.CurrentSlotInActive];

        switch (reward.RewardType)
        {
            //case RewardType.Wood:
            //    CurrencyView.Instance.AddWood(reward.CountCurrency);
            //    break;
            //case RewardType.Diamond:
            //    CurrencyView.Instance.AddWood(reward.CountCurrency);
            //    break;
            case RewardType.Gold:
                AddCredit(reward.CountCurrency);
                break;
        }

        _dailyRewardView.TimeGetReward = DateTime.UtcNow;
       _dailyRewardView.CurrentSlotInActive = (_dailyRewardView.CurrentSlotInActive + 1) % _dailyRewardView.Rewards.Count;

       RefreshRewardsState();
   }

    public void AddCredit(int value)
    {
        if(CurrencyView.Instance!=null)
            CurrencyView.Instance.AddGold(value);
        if(_credit !=null)
        _credit.Value += value;
    }
    private void ResetTimer()
   {
       PlayerPrefs.DeleteAll();
   }
}
