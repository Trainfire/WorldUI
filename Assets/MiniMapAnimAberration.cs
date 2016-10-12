using UnityEngine;
using UnityStandardAssets.ImageEffects;
using Framework;

public class MiniMapAnimAberration : MiniMapAnimation
{
    [SerializeField] private VignetteAndChromaticAberration _aberration;

    [SerializeField]
    private float _initialStrength;

    [SerializeField]
    private float _targetStrength;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private AnimationCurve _curve;

    protected override void OnPlayIn()
    {
        _aberration.chromaticAberration = _initialStrength;

        var lerp = TweenHelper.Create(_initialStrength, _targetStrength, _duration);
        lerp.OnTweenValue += OnTween;
        lerp.OnDone += OnTweenDone;
        lerp.Play();
    }

    protected override void OnPlayOut()
    {
        var lerp = new TweenFloat();
        lerp.From = _aberration.chromaticAberration;
        lerp.To = _initialStrength;
        lerp.Duration = _duration;
        lerp.OnTweenValue += OnTween;
        lerp.OnDone += OnTweenDone;
        lerp.Play();
    }

    private void OnTweenDone(Tween<float> obj)
    {
        obj.OnTweenValue -= OnTween;
        obj.OnDone -= OnTweenDone;

        if (State == AnimationState.Out)
            TriggerPlayOutComplete();
    }

    void OnTween(float v)
    {
        _aberration.chromaticAberration = v;
    }
}
