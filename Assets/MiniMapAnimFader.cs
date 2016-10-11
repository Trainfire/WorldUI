using UnityEngine;
using UnityStandardAssets.ImageEffects;
using Framework;

public class MiniMapAnimFader : MiniMapAnimation
{
    [SerializeField] private CanvasGroup _canvas;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private AnimationCurve _curve;

    protected override void OnShow()
    {
        base.OnShow();

        _canvas.alpha = 0f;

        var lerp = new TweenFloat();
        lerp.From = 0f;
        lerp.To = 1f;
        lerp.Duration = _duration;
        lerp.OnTweenValue += OnTween;
        lerp.Play();
    }

    void OnTween(float v)
    {
        _canvas.alpha = v;
    }
}
