using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// バーチャルスティックの入力状態を表示するUI
/// </summary>
public class VirtualStickView : MonoBehaviour
{
    private static readonly Color COLOR_WHITE_ALPHA = new Color32(255, 255, 255, 0);

    private static readonly Vector3 SCALE_BASE_END = new Vector3(0.2f, 0.2f, 1f);

    private static readonly Vector3 SCALE_ROOT_CLOSE = new Vector3(0.6f, 0.6f, 1f);

    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private RectTransform _scaleRoot;

    [SerializeField]
    private Image _base;

    [SerializeField]
    private Image _ring;

    [SerializeField]
    private RectTransform _axisIn;

    [SerializeField]
    private Image _handleIn;

    [SerializeField]
    private RectTransform _axisOut;

    [SerializeField]
    private Image _handleOut;

    private int _dragMaxDistance = 180;

    private RectTransform _rect;

    private Vector2 _basePosition;

    private Sequence _seqAct;

    private Color _colorBase;

    private bool _isClose;

    private Sequence _seqOpen;

    private Color _colorBaseLight;

    private Color _colorRing;

    /// <summary>
    /// 最大入力と判定するドラッグ量を設定
    /// </summary>
    /// <param name="dragMaxDistance">最大入力と判定するドラッグ量（ピクセル）</param>
    public void SetDragMaxDistance(int dragMaxDistance)
    {
        _dragMaxDistance = dragMaxDistance;
    }

    private void Awake()
    {
        _rect = transform as RectTransform;
        _colorBase = _base.color;
        _colorBaseLight = _colorBase;
        _colorBaseLight.a *= 0.7f;
        _colorRing = _ring.color;

        End(0f);
    }

    /// <summary>
    /// ドラッグ操作の開始演出
    /// </summary>
    /// <param name="dragPosition">ドラッグ座標</param>
    /// <param name="basePosition">ドラッグ開始座標</param>
    /// <param name="duration">演出時間</param>
    /// <param name="ease">イージング</param>
    public void Begin(Vector2 dragPosition, Vector2 basePosition, float duration = 0.3f, Ease ease = Ease.OutCubic)
    {
        _ring.enabled = true;
        _handleIn.enabled = true;
        _handleOut.enabled = true;

        _scaleRoot.localScale = Vector3.one;
        _base.color = _colorBase;
        _ring.color = _colorRing;

        _basePosition = basePosition;
        var dragVector = dragPosition - _basePosition;
        _axisIn.localEulerAngles = new Vector3(0f, 0f, Vector2.Angle(Vector2.up, dragVector) * Mathf.Sign(-dragVector.x));

        var beginPosition = _rect.anchoredPosition;
        var countTime = 0f;
        _seqAct?.Kill();
        _seqAct = DOTween.Sequence();
        _seqAct.Append(DOTween.To(() => 0f, t => countTime = t, 1f, duration).SetEase(ease));
        _seqAct.OnUpdate(() => _rect.anchoredPosition = Vector2.Lerp(beginPosition, _basePosition, countTime));
        _seqAct.Join(_base.DOColor(_colorBase, duration).SetEase(ease));
        _seqAct.Join(_base.rectTransform.DOScale(Vector3.one, duration).SetEase(ease));
        _seqAct.Play();
    }

    /// <summary>
    /// ドラッグ操作の終了演出
    /// </summary>
    /// <param name="duration">演出時間</param>
    /// <param name="ease">イージング</param>
    public void End(float duration = 0.3f, Ease ease = Ease.OutCubic)
    {
        _seqOpen?.Kill();
        _seqAct?.Kill();
        _seqAct = DOTween.Sequence();
        _seqAct.Append(_rect.DOAnchorPos(Vector2.zero, duration).SetEase(ease));
        _seqAct.Join(_base.DOColor(COLOR_WHITE_ALPHA, duration).SetEase(ease));
        _seqAct.Join(_base.rectTransform.DOScale(SCALE_BASE_END, duration).SetEase(ease));
        _seqAct.InsertCallback(Mathf.Max(duration - 0.1f, 0f), () =>
        {
            _ring.enabled = false;
            _handleIn.enabled = false;
            _handleOut.enabled = false;
        });
        _seqAct.Play();
    }

    /// <summary>
    /// ドラッグ操作時の表示更新
    /// </summary>
    /// <param name="dragVector">ドラッグ座標</param>
    /// <param name="basePosition">基点座標</param>
    public void SetDragVector(Vector2 dragVector, Vector2 basePosition)
    {
        _basePosition = basePosition;
        if(!_seqAct.IsActive() || !_seqAct.IsPlaying())
        {
            _rect.anchoredPosition = _basePosition;
        }

        _axisIn.localEulerAngles = new Vector3(0f, 0f, Vector2.Angle(Vector2.up, dragVector) * Mathf.Sign(-dragVector.x));

        if(dragVector.magnitude < _dragMaxDistance - 10)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    /// <summary>
    /// 入力方向とは別で方向情報を表示
    /// デバッグなどで利用してください
    /// </summary>
    /// <param name="angle">方向を表す角度</param>
    public void SetAngleAxisOut(float angle)
    {
        _axisOut.localEulerAngles = new Vector3(0f, 0f, angle);
    }

    private void Close(float duration = 0.3f, Ease ease = Ease.OutCubic)
    {
        if(_isClose)
        {
            return;
        }
        _isClose = true;
        _seqOpen?.Kill();
        _seqOpen = DOTween.Sequence();
        _seqOpen.Append(_scaleRoot.DOScale(SCALE_ROOT_CLOSE, duration).SetEase(ease));
        _seqOpen.Join(_base.DOColor(_colorBaseLight, duration).SetEase(ease));
        _seqOpen.Join(_ring.DOColor(COLOR_WHITE_ALPHA, duration).SetEase(ease));
        _seqOpen.Play();
    }

    private void Open(float duration = 0.3f, Ease ease = Ease.OutCubic)
    {
        if(!_isClose)
        {
            return;
        }
        _isClose = false;
        _seqOpen?.Kill();
        _seqOpen = DOTween.Sequence();
        _seqOpen.Append(_scaleRoot.DOScale(Vector3.one, duration).SetEase(ease));
        _seqOpen.Join(_base.DOColor(_colorBase, duration).SetEase(ease));
        _seqOpen.Join(_ring.DOColor(_colorRing, duration).SetEase(ease));
        _seqOpen.Play();
    }
}
