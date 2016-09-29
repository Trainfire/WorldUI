using UnityEngine;
using System;

namespace Framework
{
    class TimeToLive : MonoBehaviour
    {
        public event Action<TimeToLive> Destroyed;

        public float Delay { get; set; }

        private float _timeStamp;
        private bool _started;

        public void StartTimer()
        {
            _started = true;
            _timeStamp = Time.timeSinceLevelLoad;
        }

        public void Update()
        {
            if (!_started)
                return;

            if (Time.time > _timeStamp + Delay)
            {
                Destroyed.InvokeSafe(this);
                Destroy(gameObject);
            }
        }
    }
}
