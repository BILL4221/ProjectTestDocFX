using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runnex.Utilities
{
    public class Timer
    {
        public bool IsCompleted { get; private set; }
        public bool IsPaused { get; private set; }

        private float duration;
        private float startTime;
        private float lastUpdateTime;
        private Action onCompleted;

        public Timer(float duration, Action onCompleted = null)
        {
            IsCompleted = false;
            this.duration = duration;
            this.startTime = Time.time;
            this.onCompleted = onCompleted;
        }
        
        private float GetTimeDelta()
        {
            return Time.time - lastUpdateTime;
        }

        public void Update()
        {
            if (IsCompleted)
            {
                return;
            }

            if (IsPaused)
            {
                startTime += GetTimeDelta();
                lastUpdateTime = Time.time;
                return;
            }

            lastUpdateTime = Time.time;

            if (lastUpdateTime > startTime + duration)
            {
                IsCompleted = true;
                onCompleted?.Invoke();
            }
        }

        public void Pause()
        {
            if (IsCompleted)
            {
                return;
            }
            IsPaused = true;
        }

        public void Resume()
        {
            if (IsCompleted)
            {
                return;
            }
            IsPaused = false;
        }

        public void Restart()
        {
            this.startTime = Time.time;
        }

        public float GetRemainTime()
        {
            return Mathf.Max(0, (startTime + duration) - lastUpdateTime);
        }

        public float GetTimePass()
        {
            return lastUpdateTime - startTime;
        }

    }
}
