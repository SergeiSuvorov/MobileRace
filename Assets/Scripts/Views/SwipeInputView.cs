using JoostenProductions;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeInputView : GameInputView
{

    private Vector3 _startTouchPosition;
    private Vector3 _direction;
    private Vector3 _zeroVector = new Vector3(0, 0, 0);

    public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        base.Init(leftMove, rightMove, speed);
        _moveAcceleration *= 5;/// для большей динамичности
    }
    protected override void  Move()
    {
        if (Input.touchCount > 0)
        {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                    _startTouchPosition= Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    break;

                    case TouchPhase.Ended:
                    var endPos =  Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    if (_startTouchPosition != _zeroVector)
                    {
                        _direction = endPos - _startTouchPosition;
                        if(_direction.x>0)
                        {
                            _curentSpeed = _curentSpeed < _speed ? (_curentSpeed + _moveAcceleration) : _speed;
                        }
                        else if (_direction.x < 0)
                        {
                            _curentSpeed = _curentSpeed > -_speed ? (_curentSpeed - _moveAcceleration) : -_speed;
                        }
                    }
                    break;
                }
            Debug.Log(_curentSpeed);
        }

        base.Move();
    }
}
