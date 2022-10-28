using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スプライトアニメーション
/// </summary>
[RequireComponent(typeof(Image))]
public class SpriteImageAnimationView : SpriteAnimationViewBase
{
    private Image _image;

    private bool _isSetNativeSize;

    protected override void Initialize()
    {
        base.Initialize();
        _image = GetComponent<Image>();
    }

    public void Play(string spritePathFormat, int count, int startIndex = 0, float interval = 0.1f, int loops = -1, bool isSetNativeSize = false, Action<Sprite> onUpdate = null, Action onComplete = null)
    {
        _isSetNativeSize = isSetNativeSize;
        Play(spritePathFormat, count, startIndex, interval, loops, onUpdate, onComplete);
    }

    protected override void SetSprite(Sprite sprite)
    {
        _image.sprite = sprite;
        if (_isSetNativeSize)
        {
            _image.SetNativeSize();
        }
    }
}
