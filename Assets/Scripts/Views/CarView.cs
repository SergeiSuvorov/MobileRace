using DG.Tweening;
using Tools;
using UnityEngine;

public class CarView : MonoBehaviour
{
    [SerializeField] Transform _forwardWheel;
    [SerializeField] Transform _backWheel;
    private Sequence _forwardWheelSequence;
    private Sequence _backWheelSequence;
    private float _maxSpeed;
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
        sequence.Kill();
        Vector3 vector3 = wheelTransform.rotation.eulerAngles;
       
        if (speed>0)
            vector3.z -= 175;
        else
            vector3.z += 175;
        var rotateSpeed= _maxSpeed / (25 * Mathf.Abs(speed));

        sequence.Append(wheelTransform.DOLocalRotate(vector3, rotateSpeed).SetEase(Ease.Linear));
        sequence.OnComplete(() =>
        {
            WheelRotate(wheelTransform,speed,sequence);
        });
    }
} 

