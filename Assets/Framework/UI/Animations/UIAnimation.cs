using UnityEngine;
using System;

namespace Framework.Animation
{
    public enum AnimationEventType
    {
        None,
        Playing,
        Stopping,
        PlayComplete,
    }

    public class AnimationEvent : EventBase<UIAnimation>
    {
        public AnimationEventType PlaybackType { get; private set; }

        public AnimationEvent(UIAnimation sender, AnimationEventType playbackType) : base(sender)
        {
            PlaybackType = playbackType;
        }
    }

    public abstract class UIAnimation : MonoBehaviour
    {
        public event Action<AnimationEvent> Triggered;

        [SerializeField] private GameObject _target;
        [SerializeField] private float _duration;
        [SerializeField] private AnimationCurve _curve;

        protected GameObject Target { get { return _target; } }
        protected float Duration { get { return _duration; } }
        protected AnimationCurve Curve { get { return _curve; } }

        public void Play()
        {
            Triggered.InvokeSafe(new AnimationEvent(this, AnimationEventType.Playing));
            OnPlay();
        }

        public void Stop()
        {
            Triggered.InvokeSafe(new AnimationEvent(this, AnimationEventType.Stopping));
            OnStop();
        }

        protected virtual void OnPlay() { }
        protected virtual void OnStop() { }

        protected void TriggerPlayComplete()
        {
            Triggered.InvokeSafe(new AnimationEvent(this, AnimationEventType.PlayComplete));
        }
    }
}
