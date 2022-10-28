using UnityEngine;

public class SpriteAnimationSimple : MonoBehaviour
{
    [SerializeField]
    private string _spritePathFormat;

    [SerializeField]
    private SpriteAnimationViewBase.SpriteMode _spriteMode;

    [SerializeField]
    private int _count;

    [SerializeField]
    private int _startIndex;

    [SerializeField]
    private float _interval = 0.1f;

    [SerializeField]
    private int _loops = -1;

    [SerializeField]
    private bool _log = false;

    private SpriteAnimationViewBase _spriteAnimationView;

    private void Start()
    {
        _spriteAnimationView = GetComponent<SpriteAnimationViewBase>();
        PlayAnimation();
    }

    [ContextMenu("Play Animation")]
    private void PlayAnimation()
    {
        _spriteAnimationView.SetSpriteMode(_spriteMode);
        _spriteAnimationView.Play(_spritePathFormat, _count, _startIndex, _interval, _loops,
            _log ? (sprite) => Debug.Log($"OnUpdate: {sprite.name}") : null,
            _log ? () => Debug.Log("OnComplete") : null
        );
    }
}
