using Items;
using JoostenProductions;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonController
{
    private ButtonItemModel _itemButtonModel;
    private ButtonItemView _itemButtonView;
    private readonly float coolDawnTime=3f;
    private float coolDawnCurrent=0F;
    public Action<IItem> ButtonClick;

    public ItemButtonController(GameObject button, IItem item)
    {
        Text buttonsText = button.GetComponentInChildren<Text>();
        _itemButtonView = button.GetComponent<ButtonItemView>();
        _itemButtonView.ButtonClick += OnInventoryButtonClick;
        _itemButtonModel = new ButtonItemModel(item);
    }

    public ItemButtonController(ButtonItemView buttonItemView)
    {
        _itemButtonView = buttonItemView;
    }

    public void UpdateButtonAction(IItem item)
    {
        _itemButtonModel.ChangeButtonBonus(item);
        _itemButtonView.Init(item.Info.Title);
    }

    public void SetActive(bool value)
    {
        _itemButtonView.gameObject.SetActive(value);
    }
    public void OnInventoryButtonClick()
    {
        if (coolDawnCurrent > 0)
            return;
        SetActive(false);
        
        ButtonClick?.Invoke(_itemButtonModel.Item);
        coolDawnCurrent = coolDawnTime;
        UpdateManager.SubscribeToUpdate(CoolDawn);
    }

    private void CoolDawn()
    {
        if(coolDawnCurrent>0f)
        {
            coolDawnCurrent -= Time.deltaTime;
        }
        else
        {
            SetActive(true);
            UpdateManager.UnsubscribeFromUpdate(CoolDawn);
        }
    }
}

