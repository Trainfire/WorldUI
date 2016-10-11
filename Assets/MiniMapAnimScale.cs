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

    protected override void OnShow()
    {
        base.OnShow();

        var lerp = new TweenVector3();
        lerp.From = _initialScale;
        lerp.To = _targetScale;
        lerp.Duration = _duration;
        lerp.OnTweenValue += OnTween;
        lerp.Curve = _curve;
        lerp.Play();
    }

    void OnTween(Vector3 v)
    {
        transform.localScale = v;
    }
}
