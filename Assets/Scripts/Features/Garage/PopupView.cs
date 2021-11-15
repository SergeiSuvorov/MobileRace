using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class PopupView : MonoBehaviour
{
    [SerializeField]
    private Button _buttonClosePopup;

    [SerializeField]
    private float _duration = 0.3f;

    public Button ButtonClosePopup => _buttonClosePopup;

    UnityAction _onPopUpShow;
    UnityAction _onPopUpHide;
    protected void SettingPopup(UnityAction onPopUpShow, UnityAction onPopUpHide)
    {
        _buttonClosePopup.onClick.AddListener(HidePopup);
        transform.localScale = Vector3.zero;

        _onPopUpShow = onPopUpShow;
        _onPopUpHide = onPopUpHide;
        transform.localScale = Vector3.zero;
        //gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _buttonClosePopup.onClick.RemoveAllListeners();
    }

    protected void ShowPopup()
    {
        gameObject.SetActive(true);
        var sequence = DOTween.Sequence();

        sequence.Insert(0.0f, transform.DOScale(Vector3.one, _duration));
        sequence.OnComplete(() =>
        {
            _onPopUpShow?.Invoke();
            sequence = null;
        });

    }

    protected void HidePopup()
    {
        var sequence = DOTween.Sequence();

        sequence.Insert(0.0f, transform.DOScale(Vector3.zero, _duration));
        sequence.OnComplete(() =>
        {
            sequence = null;
            gameObject.SetActive(false);
            _onPopUpHide?.Invoke();
        });
    }

}
