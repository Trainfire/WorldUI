using UnityEngine;

namespace Framework.UI
{
    class UIAnimationFader : UIAnimation
    {
        [SerializeField]
        private FadeType _fadeType;

        private CanvasGroup _canvasGroup;
        private TweenFloat _tweenFloat;

        void Start()
        {
            _canvasGroup = Target.gameObject.GetOrAddComponent<CanvasGroup>();

            _tweenFloat = new TweenFloat();
            _tweenFloat.Duration = Duration;
            _tweenFloat.OnTweenValue += OnTween;
        }

        protected override void OnPlay()
        {
            base.OnPlay();

            _tweenFloat.From = _canvasGroup.alpha;
            _tweenFloat.To = _fadeType == FadeType.In ? 1f : 0f;
            _tweenFloat.Play();
        }

        protected override void OnStop()
        {
            base.OnStop();
            _tweenFloat.Stop();
        }

        void OnTween(float alpha)
        {
            _canvasGroup.alpha = alpha;
        }
    }
}
