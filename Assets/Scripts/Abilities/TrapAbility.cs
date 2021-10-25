using System;
using UnityEngine;

public class TrapAbility : IAbility
{
    private readonly Rigidbody2D _viewPrefab;
    private float _speed;

    public TrapAbility(float speed, ResourcePath viewPath)
    {
        _speed = speed;
        var go = ResourceLoader.LoadPrefab(viewPath);
        _viewPrefab = go.GetComponent<Rigidbody2D>();
        if (_viewPrefab == null)
            throw new ArgumentException("Unable to load view");
    }

  

    public void Apply(IAbilityActivator activator)
    {
        var projectile = GameObject.Instantiate(_viewPrefab);
        projectile.AddForce(-activator.GetViewObject().transform.right * _speed, ForceMode2D.Force);
    }

}
