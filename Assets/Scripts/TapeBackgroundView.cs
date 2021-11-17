using System.Collections.Generic;
using Tools;
using UnityEngine;

public class TapeBackgroundView : MonoBehaviour, ISpiteAddressable
{
    [SerializeField] 
    private Background[] _backgrounds;
    [SerializeField] private List<DataSpriteAddressable> _addressableSprites;

    private IReadOnlySubscriptionProperty<float> _diff;
    public List<DataSpriteAddressable> AddressableSprites => _addressableSprites;

    public void Init(IReadOnlySubscriptionProperty<float> diff)
    {
        _diff = diff;
        _diff.SubscribeOnChange(Move);

        for(int i=0;i<_backgrounds.Length; i++)
        {
            _backgrounds[i].Init();
        }
    }

    protected void OnDestroy()
    {
        _diff?.SubscribeOnChange(Move);
    }

    private void Move(float value)
    {
        foreach (var background in _backgrounds)
            background.Move(-value);
    }
}

