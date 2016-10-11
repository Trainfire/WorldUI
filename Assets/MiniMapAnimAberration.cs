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

    protected override void OnShow()
    {
        base.OnShow();

        _aberration.chromaticAberration = _initialStrength;

        var lerp = new TweenFloat();
        lerp.From = _initialStrength;
        lerp.To = _targetStrength;
        lerp.Duration = _duration;
        lerp.OnTweenValue += OnTween;
        lerp.Play();
    }

    void OnTween(float v)
    {
        _aberration.chromaticAberration = v;
    }
}
