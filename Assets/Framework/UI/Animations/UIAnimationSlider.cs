using UnityEngine;

namespace Framework.UI
{
    class UIAnimationSlider : UIAnimation
    {
        [SerializeField]
        private Vector2 _position;

        private TweenVector2 _tweenVec;

        void Start()
        {
            _tweenVec = new TweenVector2();
            _tweenVec.OnTweenValue += OnTween;
        }

        protected override void OnPlay()
        {
            base.OnPlay();

            _tweenVec.Duration = Duration;
            _tweenVec.From = Target.anchoredPosition;
            _tweenVec.To = _position;
            _tweenVec.Play();
        }

        protected override void OnStop()
        {
            base.OnStop();
            _tweenVec.Stop();
        }

        void OnTween(Vector2 v)
        {
            Target.anchoredPosition = v;
        }
    }
}
