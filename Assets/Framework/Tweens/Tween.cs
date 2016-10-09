using System;
using UnityEngine;
using UnityEngine.Events;

namespace Framework
{
    public abstract class Tween<T> : IMonoUpdateReceiver
    {
        public UnityAction<T> OnTweenValue;
        public float Duration;
        public AnimationCurve Curve;
        public bool DoTween = false;

        public T Value { get; private set; }
        public T From { get; set; }
        public T To { get; set; }

        public bool Tweening { get; set; }
        float CurrentTime { get; set; }
        UnityAction OnDone { get; set; }

        public Tween()
        {
            MonoEventRelay.RegisterForUpdate(this);
        }

        public void Play(UnityAction onDone = null)
        {
            CurrentTime = 0f;
            DoTween = true;
            OnDone = onDone;
        }

        public void Stop()
        {
            DoTween = false;
        }

        protected abstract T OnTween(float delta);

        T DefaultCurve(T value)
        {
            return value;
        }

        void IMonoUpdateReceiver.OnUpdate()
        {
            if (DoTween)
            {
                DoTween = false;
                Tweening = true;
                CurrentTime = 0f;
            }

            if (Tweening)
            {
                float t = CurrentTime / Duration;

                if (Curve != null)
                    t = Curve.Evaluate(t);

                Value = OnTween(t);

                if (OnTweenValue != null)
                    OnTweenValue(Value);

                CurrentTime += Time.deltaTime;

                if (CurrentTime > Duration)
                {
                    Value = To;

                    Tweening = false;

                    if (OnDone != null)
                        OnDone();
                }
            }
        }
    }
}
