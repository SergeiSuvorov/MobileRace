using JoostenProductions;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public abstract class GameInputView : BaseInputView
{
    protected Vector3 _startPosition;
    protected float _moveAcceleration;

    private float _curentSpeed;
    private float _curentMaxSpeed;
    public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, SubscriptionProperty<float> speed)
    {
        base.Init(leftMove, rightMove, speed);
        _speed.SubscribeOnChange(onMaxSpeedChange);
        _curentMaxSpeed = _speed.Value;
        _startPosition = transform.position;
        _curentSpeed = 0.0f;
        _moveAcceleration = 0.05f;//для проверки
        UpdateManager.SubscribeToUpdate(OnUpdate);
    }

    private void OnUpdate()
    {
        CheckInput();
        Move();
    }
    protected void AddAcceleration(bool isForvardAcceleration)
    {
        if(isForvardAcceleration)
            _curentSpeed = _curentSpeed < _speed.Value ? (_curentSpeed + _moveAcceleration) : _speed.Value;
        else
            _curentSpeed = _curentSpeed > -_speed.Value ? (_curentSpeed - _moveAcceleration) : -_speed.Value;
    }

    protected virtual void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdate);
        _speed.UnSubscriptionOnChange(onMaxSpeedChange);
    }

    private void onMaxSpeedChange(float speed)
    {
      if(speed>_curentMaxSpeed)
      {
            _curentSpeed += speed - _curentMaxSpeed;
      }
        _curentMaxSpeed = speed;
    }

    /// <summary>
    /// Движение объектов на сцене. Данный метод необходио подключить к UpdateManager-у.
    /// </summary>
    private void Move()
    {
        OnRightMove(0.02f*_curentSpeed);//0.02 для равномерности движения, иначе слишком резко
    }

    protected abstract void CheckInput();
}
