using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public class VectorDeltaWatch
    {
        private float interval;
        private bool started = false;
        private Vector3 target = Vector3.zero;

        public VectorDeltaWatch(float interval)
        {
            this.interval = interval * interval;
        }

        public bool IsStarted()
        {
            return this.started;
        }

        public void Reset()
        {
            target = Vector3.zero;
            started = false;
        }

        public bool Ping(Vector3 position)
        {
            if (started == false)
            {
                started = true;
                target = position;
            }
            if (position == target) return false;
            float sqt = (position - target).sqrMagnitude;
            if (sqt >= interval)
            {
                target = position;
                return true;
            }

            return false;
        }
    }
}
