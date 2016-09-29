using UnityEngine;

namespace Framework.UI
{
    abstract class UIAnimation : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _target;

        [SerializeField]
        private float _duration;

        protected RectTransform Target { get { return _target; } }
        protected float Duration { get { return _duration; } }

        public void Play()
        {
            OnPlay();
        }

        public void Stop()
        {
            OnStop();
        }

        protected virtual void OnPlay() { }
        protected virtual void OnStop() { }
    }
}
