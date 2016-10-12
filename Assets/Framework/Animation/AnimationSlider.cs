using UnityEngine;

namespace Framework.Animation
{
    class AnimationSlider : AnimationBase
    {
        [SerializeField]
        private Vector2 _position;

        private RectTransform _rectTransform;
        private TweenVector2 _tweenVec;

        void Start()
        {
            _rectTransform = Target.GetComponent<RectTransform>();

            _tweenVec = gameObject.AddComponent<TweenVector2>();
            _tweenVec.OnTweenValue += OnTween;
            _tweenVec.OnDone += OnTweenDone;
        }

        private void OnTweenDone(Tween<Vector2> obj)
        {
            TriggerPlayComplete();
        }

        protected override void OnPlay()
        {
            base.OnPlay();

            if (_rectTransform != null)
            {
                _tweenVec.Duration = Duration;
                _tweenVec.From = _rectTransform.anchoredPosition;
                _tweenVec.To = _position;
                _tweenVec.Play();
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
            _tweenVec.Stop();
        }

        void OnTween(Vector2 v)
        {
            _rectTransform.anchoredPosition = v;
        }
    }
}
