using DG.Tweening;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class CarView : MonoBehaviour, ISpiteAddressable
{
    [SerializeField] private Transform _forwardWheel;
    [SerializeField] private Transform _backWheel;
    [SerializeField] private List<DataSpriteAddressable> _addressableSprites;
    private Sequence _forwardWheelSequence;
    private Sequence _backWheelSequence;
    private float _maxSpeed;
    public List<DataSpriteAddressable> AddressableSprites => _addressableSprites;

    public void Init(SubscriptionProperty<float> moveDiff, float maxSpeed)
    {
        moveDiff.SubscribeOnChange( WheelsRotate);
        _forwardWheelSequence = DOTween.Sequence();
        _backWheelSequence = DOTween.Sequence();
        _maxSpeed = maxSpeed;
    }

    private void WheelsRotate(float speed)
    {
        _forwardWheelSequence.Pause();
        _backWheelSequence.Pause();
        if (Mathf.Abs(speed) >= 0.01f)
        {
            WheelRotate(_forwardWheel.transform, speed, _forwardWheelSequence);
            WheelRotate(_backWheel.transform, speed, _backWheelSequence);
        }
    }
    private void WheelRotate(Transform wheelTransform, float speed, Sequence sequence)
    {
        sequence.Play();
        Vector3 vector3 = wheelTransform.rotation.eulerAngles;
       
        if (speed>0)
            vector3.z -= 175;
        else
            vector3.z += 175;
        var rotateSpeed= _maxSpeed / (25 * Mathf.Abs(speed));

        sequence.Append(wheelTransform.DOLocalRotate(vector3, rotateSpeed).SetEase(Ease.Linear));
        sequence.OnComplete(() =>
        {
            sequence.SetLoops(-1);
        });
    }
}





