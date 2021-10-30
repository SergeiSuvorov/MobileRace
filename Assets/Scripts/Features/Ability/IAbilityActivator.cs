using UnityEngine;

public interface IAbilityActivator
{
    GameObject GetViewObject();
    void ActivateAbility(IAbility ability, float power);
}