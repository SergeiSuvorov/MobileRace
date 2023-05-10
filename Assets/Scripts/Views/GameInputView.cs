using JoostenProductions;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class GameInputView : BaseInputView
{
    protected Vector3 _startPosition;
    protected float _curentSpeed;
    protected float _moveAcceleration;
    public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        base.Init(leftMove, rightMove, speed);
        _startPosition = transform.position;
        _curentSpeed = 0.0f;
        _moveAcceleration = 0.05f;//для проверки
        UpdateManager.SubscribeToUpdate(Move);
    }

    protected virtual void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(Move);
    }

    /// <summary>
    /// Движение объектов на сцене. Данный метод необходио подключить к UpdateManager-у.
    /// </summary>
    protected virtual void Move()
    {
        OnRightMove(0.02f*_curentSpeed);//0.02 для равномерности движения, иначе слишком резко
    }
}
