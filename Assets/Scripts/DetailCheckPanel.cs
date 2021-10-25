using Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailCheckPanel: MonoBehaviour
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Text _itemName;
    private List<UpgradeItemConfig> _upgradeItemList;
    private UpgradeItemConfig _checkItem;
    public void Init(List<UpgradeItemConfig> upgradeItemList)
    {
        _upgradeItemList = upgradeItemList;
        _checkItem = _upgradeItemList[0];
        _itemName.text = _checkItem.ItemConfig.Title;
        _nextButton.onClick.AddListener(onNextButtonClick);
        _prevButton.onClick.AddListener(onPrevButtonClick);
    }

    private void onPrevButtonClick()
    {
        int index = _upgradeItemList.IndexOf(_checkItem);
        if (index > 0)
        {
            _checkItem = _upgradeItemList[--index];
            _itemName.text = _checkItem.ItemConfig.Title;
        }
    }

    private void onNextButtonClick()
    {
        int index = _upgradeItemList.IndexOf(_checkItem);
        if (index+1 < _upgradeItemList.Count)
        {
            _checkItem = _upgradeItemList[++index];
            _itemName.text = _checkItem.ItemConfig.Title;
        }
    }

    public UpgradeItemConfig GetCheckItem()
    {
        return _checkItem;
    }
    private void OnDestroy()
    {
        if (_upgradeItemList != null)
        {
            _upgradeItemList.Clear();
            _upgradeItemList = null;
        }
    }
}