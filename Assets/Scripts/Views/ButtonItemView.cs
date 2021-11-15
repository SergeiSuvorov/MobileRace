using Items;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonItemView : MonoBehaviour
{

    [SerializeField] private Button _button;
    [SerializeField] private Text _buttonText;

    public Action ButtonClick;

    public void Init(string  itemName)
    {
        _buttonText.text = itemName;
        _button?.onClick.AddListener(OnItemButtonClick);
    }

    /// <summary>
    /// Событие сигнализирующее о нажатия кнопки
    /// </summary>
    private void OnItemButtonClick()
    { 
        ButtonClick?.Invoke();
    }

    private void OnDisable()
    {
        _button?.onClick.RemoveAllListeners();
    }

    private void OnEnable()
    {
        _button?.onClick.AddListener(OnItemButtonClick);
    }

    public void ChangeButtonText(string buttonText)
    {
        _buttonText.text = buttonText;
    }
}

