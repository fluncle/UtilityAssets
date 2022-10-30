using UnityEngine;
using DG.Tweening;

public class LoagingLayer : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _group;

    [SerializeField]
    private GameObject _textObj;

    [SerializeField]
    private RectTransform _circleRect;

    private Sequence _rollSeq;

    public void Play()
    {
        _group.alpha = 1f;
        _textObj.SetActive(true);
        gameObject.SetActive(true);

        _rollSeq = DOTween.Sequence();
        _rollSeq.AppendCallback(() => AddEulerAngleZ(_circleRect, -40f));
        _rollSeq.AppendInterval(0.1f);
        _rollSeq.SetLoops(-1);
    }

    private void AddEulerAngleZ(RectTransform rect, float diff)
    {
        var eulerAngles = rect.eulerAngles;
        eulerAngles.z += diff;
        rect.eulerAngles = eulerAngles;
    }

    public void End()
    {
        _textObj.SetActive(false);

        var seq = DOTween.Sequence();
        seq.Append(_group.DOFade(0f, 0.3f));
        seq.OnComplete(() =>
        {
            _rollSeq?.Kill();
            gameObject.SetActive(false);
        });
    }
}
