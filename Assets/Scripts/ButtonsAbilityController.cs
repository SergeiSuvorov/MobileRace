using Inventory;
using Items;
using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class ButtonsAbilityController: BaseController, IAbilityCollectionView
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/AbilityUI" };
    private List<ItemButtonController> buttonsPool = new List<ItemButtonController>();
    private GameObject button;
   
    public event EventHandler<IItem> UseRequested;

    public ButtonsAbilityController(Transform placeForUi)
    {
        var objView = UnityEngine.Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath),placeForUi, false);
        AddGameObjects(objView);
        button = objView.GetComponentInChildren<ButtonItemView>().gameObject;

        for (int i = 0; i < 10; i++)
        {
            AddNewButtonInPool();
        }

        button.SetActive(false);
    }


    private void AddNewButtonInPool()
    {
        GameObject newButton = UnityEngine.Object.Instantiate(button, button.transform.position, button.transform.rotation, button.transform.parent);
        ItemButtonController newButtonController = new ItemButtonController(newButton,null);
        newButtonController.ButtonClick += onButtonClick;
        buttonsPool.Add(newButtonController);
        newButton.SetActive(false);
    }

   
    public void onButtonClick(IItem Item)
    {
        UseRequested?.Invoke(this,Item);
    }

    public void Display(IReadOnlyList<IItem> abilityItems)
    {
        if (abilityItems.Count < 0)
            return;

        if(abilityItems.Count> buttonsPool.Count)
        {
            while(abilityItems.Count >= buttonsPool.Count)
            {
                AddNewButtonInPool();
            }
        }

        for (int i = 0; i < buttonsPool.Count; i++)
        {
            if (i < abilityItems.Count)
            {
                buttonsPool[i].SetActive(true);
                buttonsPool[i].UpdateButtonAction(abilityItems[i]);
            }
            else
                buttonsPool[i].SetActive(false);

        }
    }
}

