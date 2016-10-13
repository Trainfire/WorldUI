using UnityEngine;
using UnityStandardAssets.ImageEffects;
using Framework;

public class MiniMapAnimCamera : MiniMapAnimation
{
    [SerializeField] private Fisheye _fisheye;

    [SerializeField]
    private Vector2 _initialStrength;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private AnimationCurve _curve;

    protected override void OnPlayIn()
    {
        base.OnPlayIn();

        _fisheye.strengthX = _initialStrength.x;
        _fisheye.strengthY = _initialStrength.y;

        var lerp = new TweenVector2();
        lerp.From = _initialStrength;
        lerp.To = Vector2.zero;
        lerp.Duration = _duration;
        lerp.OnTweenValue += OnTween;
        lerp.OnDone += OnTweenDone;
        lerp.Play();
    }

    void OnTweenDone(Tween<Vector2> tweener)
    {
        tweener.OnTweenValue -= OnTween;
        tweener.OnDone -= OnTweenDone;
        TriggerPlayOutComplete();
    }

    void OnTween(Vector2 v)
    {
        Debug.Log(v);
        _fisheye.strengthX = v.x;
        _fisheye.strengthY = v.y;
    }
}
