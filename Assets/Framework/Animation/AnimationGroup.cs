using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

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
                anim.Triggered += OnAnimationEvent;
            }
        }

        public void Play()
        {
            UpdateQueue();
        }

        public void Stop()
        {
            _animations.ForEach(x => x.Stop());
        }

        void UpdateQueue()
        {
            var playbackQueue = _animations.Where(x => x.State == AnimationPlaybackState.Stopped).ToList();

            foreach (var item in playbackQueue)
            {
                item.Play();

                if (item.WaitForCompletion)
                    break;
            }
        }

        void OnAnimationEvent(AnimationEvent obj)
        {
            if(obj.PlaybackType == AnimationEventType.PlayComplete)
            {
                if (_animations.TrueForAll(x => x.State == AnimationPlaybackState.PlayComplete))
                {
                    Completed.InvokeSafe(this);
                }
                else
                {
                    UpdateQueue();
                }
            }
        }
    }
}
