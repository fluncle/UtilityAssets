using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スプライトアニメーション
/// </summary>
public abstract class SpriteAnimationViewBase : MonoBehaviour
{
    public enum SpriteMode
    {
        Single = 0,
        Multiple,
    }

    private SpriteMode _spriteMode;

    private List<Sprite> _sprites;

    private float _interval;

    private int _loops = -1;

    private Action<Sprite> _onUpdate;

    private Action _onComplete;

    private float _countTime;

    private int _currentSequence;

    private int _countLoop;

    private void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _sprites = new List<Sprite>();
        Pause();
    }

    public void SetSpriteMode(SpriteMode mode)
    {
        _spriteMode = mode;
    }

    public void Play(string spritePathFormat, int count, int startIndex = 0, float interval = 0.1f, int loops = -1, Action<Sprite> onUpdate = null, Action onComplete = null)
    {
        _sprites.Clear();

        switch (_spriteMode)
        {
            case SpriteMode.Single:
                for (int i = 0; i < count; i++)
                {
                    var sprite = Resources.Load<Sprite>(string.Format(spritePathFormat, i + startIndex));
                    _sprites.Add(sprite);
                }
                break;

            case SpriteMode.Multiple:
                var sprites = Resources.LoadAll<Sprite>(spritePathFormat);
                for (int i = 0; i < count; i++)
                {
                    var sprite = sprites[i + startIndex];
                    _sprites.Add(sprite);
                }
                break;
        }

        _interval = interval;
        _loops = loops;
        _onUpdate = onUpdate;
        _onComplete = onComplete;

        _countTime = 0f;
        _currentSequence = 0;
        _countLoop = 0;

        SetSprite(_sprites[0]);

        enabled = true;
    }

    protected abstract void SetSprite(Sprite sprite);

    public void Pause()
    {
        enabled = false;
    }

    private void Update()
    {
        _countTime += Time.deltaTime;
        if (_countTime < _interval)
        {
            return;
        }
        _countTime = 0f;

        _currentSequence++;
        if (_sprites.Count <= _currentSequence)
        {
            _currentSequence = 0;
            _countLoop++;
            if (_loops >= 0 && _loops <= _countLoop)
            {
                enabled = false;
                _onComplete?.Invoke();
                return;
            }
        }

        var sprite = _sprites[_currentSequence];
        SetSprite(sprite);
        _onUpdate?.Invoke(sprite);
    }
}
