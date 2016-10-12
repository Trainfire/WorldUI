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

    protected override void OnPlayIn()
    {
        _canvas.alpha = 0f;

        var lerp = new TweenFloat();
        lerp.From = 0f;
        lerp.To = 1f;
        lerp.Duration = _duration;
        lerp.OnTweenValue += OnTween;
        //lerp.OnDone += OnTweenDone;
        lerp.Play();
    }

    protected override void OnPlayOut()
    {
        var lerp = new TweenFloat();
        lerp.From = 1f;
        lerp.To = 0f;
        lerp.Duration = _duration;
        lerp.OnTweenValue += OnTween;
        lerp.OnDone += OnTweenDone;
        lerp.Play();
    }

    void OnTweenDone(Tween<float> obj)
    {
        obj.OnTweenValue -= OnTween;
        obj.OnDone -= OnTweenDone;
        //TriggerPlayOutComplete();
    }

    void OnTween(float v)
    {
        _canvas.alpha = v;
    }
}
