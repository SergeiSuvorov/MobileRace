using Tools;
using UnityEngine;

public abstract class BaseInputView : MonoBehaviour
{
    private SubscriptionProperty<float> _leftMove;
    private SubscriptionProperty<float> _rightMove;
    protected SubscriptionProperty<float> _speed;
    
    public virtual void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, SubscriptionProperty<float> speed)
    {
        _leftMove = leftMove;
        _rightMove = rightMove;
        _speed = speed;
    }
    
    protected void OnLeftMove(float value)
    {
        _leftMove.Value = value;
    }

    protected void OnRightMove(float value)
    {
        _rightMove.Value = value;
    }
}

