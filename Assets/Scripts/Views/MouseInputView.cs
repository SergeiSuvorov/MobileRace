using JoostenProductions;
using Tools;
using UnityEngine;

public class MouseInputView : GameInputView
{
    
    protected override void Move()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mousePos.x > _startPosition.x)
            {
                _curentSpeed = _curentSpeed < _speed ? (_curentSpeed + _moveAcceleration) : _speed;
            }
            if (mousePos.x < _startPosition.x)
            {
                _curentSpeed = _curentSpeed > -_speed ? (_curentSpeed - _moveAcceleration) : -_speed;
            }
        }

        base.Move();
    }

}

