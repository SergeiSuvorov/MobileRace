using JoostenProductions;
using Tools;
using UnityEngine;

public class TouchInputView : GameInputView
{
    protected override void Move()
    {
        if (Input.touchCount > 0)
        {
            int leftTouchCount = 0;
            int rightTouchCount = 0;
            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector3 touchPos;
                touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                if (touchPos.x > _startPosition.x)
                {
                    rightTouchCount++;
                }
                if (touchPos.x < _startPosition.x)
                {
                    leftTouchCount++;
                }
            }

            if (rightTouchCount > leftTouchCount)
            {
                _curentSpeed = _curentSpeed < _speed ? (_curentSpeed + _moveAcceleration) : _speed;
            }
            else if (rightTouchCount < leftTouchCount)
            {
                _curentSpeed = _curentSpeed > -_speed ? (_curentSpeed - _moveAcceleration) : -_speed;
            }
        }

        base.Move();
    }
}

