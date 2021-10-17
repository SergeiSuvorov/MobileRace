using JoostenProductions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailView : MonoBehaviour
{
    public virtual void Init()
    {
        gameObject.SetActive(false);
        UpdateManager.SubscribeToUpdate(TrailMoving);
    }

    /// <summary>
    /// Данный метод используется для отбражение трайлов. Данный метод необходио подключить к UpdateManager-у.
    /// </summary>
    protected virtual void TrailMoving()
    {
      
    }

    protected virtual void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(TrailMoving);
    }
}
