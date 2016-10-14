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
        private Queue<AnimationBase> _queue;

        void Awake()
        {
            _animations = new List<AnimationBase>();

            foreach (var anim in GetComponents<AnimationBase>())
            {
                _animations.Add(anim);
                anim.Triggered += OnAnimationTriggered;
            }

            _queue = new Queue<AnimationBase>();
        }

        public void Play()
        {
            if (_animations.Any(x => x.WaitForCompletion))
            {
                Debug.Log("Using queue system...");
                _queue.Clear();
                _animations.ForEach(x => _queue.Enqueue(x));
                _queue.Peek().Play();
            }
            else
            {
                _animations.ForEach(x => x.Play());
            }
        }

        public void Stop()
        {
            _animations.ForEach(x => x.Stop());
        }

        void OnAnimationTriggered(AnimationEvent animEvent)
        {
            if (animEvent.PlaybackType == AnimationEventType.PlayComplete)
            {
                if (_animations.Any(x => x.WaitForCompletion))
                {
                    if (animEvent.Sender == _queue.Peek())
                    {
                        _queue.Dequeue();

                        if (_queue.Count == 0)
                        {
                            Completed.InvokeSafe(this);
                        }
                        else
                        {
                            var next = _queue.Peek();
                            Debug.Log("Playing next anim: " + next.GetType().Name);
                            next.Play();
                        }
                    }
                }
                else if (_animations.TrueForAll(x => x.State == AnimationPlaybackState.Stopped))
                {
                    Completed.InvokeSafe(this);
                }
            }
        }
    }
}
