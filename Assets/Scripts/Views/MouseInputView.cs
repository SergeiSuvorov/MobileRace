using JoostenProductions;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInputView : GameInputView
{
    protected override void CheckInput()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mousePos.x > _startPosition.x)
            {
                AddAcceleration(true);
            }
            else if (mousePos.x < _startPosition.x)
            {
                AddAcceleration(false);
            }
        }
    }
}

