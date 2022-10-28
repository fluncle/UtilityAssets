using UnityEngine;

/// <summary>
/// SpriteRenderer用のスプライトアニメーションを再生するコンポーネント
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRendererAnimationView : SpriteAnimationViewBase
{
    private SpriteRenderer _renderer;

    protected override void Initialize()
    {
        base.Initialize();
        _renderer = GetComponent<SpriteRenderer>();
    }

    protected override void SetSprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }
}
