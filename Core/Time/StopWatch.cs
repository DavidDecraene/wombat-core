using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public class StopWatch
    {
        private float lastCheck;
        private readonly float checkPause;

        public StopWatch(float interval)
        {
            this.checkPause = interval;
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
            float delta = Time.realtimeSinceStartup - lastCheck;
            if (delta < checkPause) return false;
            lastCheck = Time.realtimeSinceStartup;
            return true;
        }

    }
}
