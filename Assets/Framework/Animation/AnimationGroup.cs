using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;
using Framework;

namespace Framework.Animation
{
    public class AnimationGroup : MonoBehaviour
    {
        public event Action<AnimationGroup> Completed;

        private List<AnimationBase> _animations;

        public void Play()
        {            
            _animations = GetComponents<AnimationBase>().ToList();

            foreach (var anim in _animations)
            {
                anim.Triggered += OnAnimationTriggered;
                anim.Play();
            }
        }

        public void Stop()
        {
            if (_animations != null)
            {
                foreach (var anim in _animations)
                {
                    anim.Triggered -= OnAnimationTriggered;
                    anim.Stop();
                }
            }
        }

        void OnAnimationTriggered(AnimationEvent animEvent)
        {
            Debug.LogFormat("Anim '{0}' in group '{1}' changed to '{2}'", animEvent.Sender.GetType(), name, animEvent.PlaybackType);

            if (animEvent.PlaybackType == AnimationEventType.PlayComplete && _animations.Contains(animEvent.Sender))
            {
                _animations.Remove(animEvent.Sender);
                animEvent.Sender.Triggered -= OnAnimationTriggered;
            }

            if (_animations.Count == 0)
                Completed.InvokeSafe(this);
        }
    }
}
