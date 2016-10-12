using UnityEngine;
using Framework;
using System;

public enum AnimationState
{
    In,
    Out
}

public abstract class MiniMapAnimation : MonoBehaviour
{
    public event Action<MiniMapAnimation> OnPlayOutComplete;

    [SerializeField]
    private bool _enabled = true;

    public AnimationState State { get; private set; }

    public void PlayIn()
    {
        State = AnimationState.In;

        if (_enabled)
            OnPlayIn();
    }

    public void PlayOut()
    {
        State = AnimationState.Out;

        if (_enabled)
            OnPlayOut();
    }

    protected virtual void OnPlayIn() { }
    protected virtual void OnPlayOut()
    {
        TriggerPlayOutComplete();
    }

    protected void TriggerPlayOutComplete()
    {
        OnPlayOutComplete.InvokeSafe(this);
    }
}
