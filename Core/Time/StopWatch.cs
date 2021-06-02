using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public class GenericStopWatch
    {
        private float lastCheck;
        private readonly float checkPause;
        public bool Paused { get; set; }

        public GenericStopWatch(float interval )
        {
            this.checkPause = interval;
        }

        public GenericStopWatch Pause(bool state)
        {
            this.Paused = state;
            return this;
        }

        public void Start(float realtimeSinceStartup)
        {
            lastCheck = realtimeSinceStartup;

        }

        public void Reset(float realtimeSinceStartup)
        {
            lastCheck = realtimeSinceStartup;

        }

        public bool Ping(float realtimeSinceStartup)
        {
            if (Paused) return false;
            float delta = realtimeSinceStartup - lastCheck;
            if (delta < checkPause) return false;
            lastCheck = realtimeSinceStartup;
            return true;
        }

    }

    public class StopWatch
    {
        private float lastCheck;
        private readonly float checkPause;
        public bool Paused { get; set; }

        public StopWatch(float interval)
        {
            this.checkPause = interval;
        }

        public StopWatch Pause(bool state)
        {
            this.Paused = state;
            return this;
        }

        public void Start()
        {
            lastCheck = Time.realtimeSinceStartup;

        }

        public void Reset()
        {
            lastCheck = Time.realtimeSinceStartup;

        }

        public bool Ping()
        {
            if (Paused) return false;
            float delta = Time.realtimeSinceStartup - lastCheck;
            if (delta < checkPause) return false;
            lastCheck = Time.realtimeSinceStartup;
            return true;
        }

    }
}
