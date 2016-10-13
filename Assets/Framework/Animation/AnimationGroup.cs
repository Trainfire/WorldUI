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

        void Awake()
        {
            _animations = new List<AnimationBase>();

            foreach (var anim in GetComponents<AnimationBase>())
            {
                _animations.Add(anim);
                anim.Triggered += OnAnimationTriggered;
            }
        }

        public void Play()
        {
            _animations.ForEach(x => x.Play());
        }

        public void Stop()
        {
            _animations.ForEach(x => x.Stop());
        }

        void OnAnimationTriggered(AnimationEvent animEvent)
        {
            //Debug.LogFormat("Anim '{0}' in group '{1}' changed to '{2}'", animEvent.Sender.GetType(), name, animEvent.PlaybackType);

            if (animEvent.PlaybackType == AnimationEventType.PlayComplete && _animations.TrueForAll(x => x.State == AnimationPlaybackState.Stopped))
                Completed.InvokeSafe(this);
        }
    }
}
