using Items;

public class ButtonItemModel
    {

 
    private IItem _item;// предмет инветаря, активируемый при нажатии на кнопку
    public IItem Item { get { return _item; } }

        public ButtonItemModel(IItem item)
        {
            _item = item;
        }


    /// <summary>
    /// Изменение предмета,активируемого кнопкой.
    /// </summary>
    /// <param name="item"></param>
    public void ChangeButtonBonus(IItem item)
    {
        _item = item;
    }
}

