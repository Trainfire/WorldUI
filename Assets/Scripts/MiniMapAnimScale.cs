using UnityEngine;
using Framework;

public class MiniMapAnimScale : MiniMapAnimation
{
    [SerializeField]
    private AnimationCurve _curve;
    [SerializeField]
    private Vector3 _initialScale;
    [SerializeField]
    private Vector3 _targetScale;
    [SerializeField]
    private float _duration;

    protected override void OnPlayIn()
    {
        base.OnPlayIn();

        var lerp = new TweenVector3();
        lerp.From = _initialScale;
        lerp.To = _targetScale;
        lerp.Duration = _duration;
        lerp.OnTweenValue += OnTween;
        lerp.Curve = _curve;
        lerp.OnDone += OnLerpDone;
        lerp.Play();
    }

    protected override void OnPlayOut()
    {
        var lerp = new TweenVector3();
        lerp.From = _targetScale;
        lerp.To = _initialScale;
        lerp.Duration = _duration;
        lerp.OnTweenValue += OnTween;
        lerp.Curve = _curve;
        lerp.OnDone += OnLerpDone;
        lerp.Play();
    }

    void OnLerpDone(Tween<Vector3> tweener)
    {
        tweener.OnDone -= OnLerpDone;

        if (State == AnimationState.Out)
            TriggerPlayOutComplete();
    }

    void OnTween(Vector3 v)
    {
        transform.localScale = v;
    }
}
