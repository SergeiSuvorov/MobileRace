using Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GarageView : PopupView
{
    [SerializeField] private DetailCheckPanel _tireViewPanel;
    [SerializeField] private DetailCheckPanel _engineViewPanel;
    [SerializeField] private Button applyButton;
    public Action<List<UpgradeItemConfig>> Apply;

    public void Init(List<UpgradeItemConfig> upgradeItemList, UnityAction onPopUpShow, UnityAction onPopUpHide)
    {
        applyButton.onClick.AddListener(onApplyButtonClick);
        var tireList = upgradeItemList.FindAll(a => a.DetailType == DetailType.Tire);
        _tireViewPanel.Init(tireList);

        var engineList = upgradeItemList.FindAll(a => a.DetailType == DetailType.Engine);
        _engineViewPanel.Init(engineList);

        SettingPopup(onPopUpShow, onPopUpHide);
        ShowPopup();
    }

    private void onApplyButtonClick()
    {
        List<UpgradeItemConfig> upgradeItemConfigs = new List<UpgradeItemConfig>();
        upgradeItemConfigs.Add(_tireViewPanel.GetCheckItem());
        upgradeItemConfigs.Add(_engineViewPanel.GetCheckItem());

        Apply?.Invoke(upgradeItemConfigs);
    }
}
