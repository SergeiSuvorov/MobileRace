using JoostenProductions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrailView : MonoBehaviour
{
    public virtual void Init()
    {
        gameObject.SetActive(false);
        UpdateManager.SubscribeToUpdate(onUpdate);
    }

    /// <summary>
    /// Данный метод используется для отбражение трайлов. Данный метод необходио подключить к UpdateManager-у.
    /// </summary>
    protected abstract void onUpdate();

    protected void SetTrailPossition(Vector3 inputCoordinate)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(inputCoordinate);
        transform.position = pos;
    }


    protected virtual void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(onUpdate);
    }
}
