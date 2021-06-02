using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public class DeltaWatch
    {
        private float timepassed = 0;
        private float interval;
        private bool started = false;
        private System.Random rd;
        private float randomInterval = 0;
        private float[] randomBounds = { 0, 0 };
        private bool startImmediate = false;
        public bool Enabled { get; set; } = true;
        public float  Timepassed { get => timepassed; }
        public float TotalTime { get => interval + randomInterval; }
        public float TimeLeft { get => TotalTime - Timepassed; }

        public DeltaWatch(float interval)
        {
            this.interval = interval;
        }

        public DeltaWatch Interval(float interval)
        {
            this.interval = interval;
            return this;
        }

        public DeltaWatch Immediate()
        {
            this.startImmediate = true;
            return this;
        }

        public DeltaWatch AddRandomRange(System.Random rd, float min, float max)
        {
            randomBounds[0] = min;
            randomBounds[1] = max;
            this.rd = rd;
            return this;
        }

        public bool IsStarted()
        {
            return this.started;
        }

        public bool IsRunning()
        {
            return this.timepassed > 0;
        }

        public void Reset(bool resetStart)
        {
            this.timepassed = 0;

            if (rd != null)
            {
                randomInterval = rd.NextFloatRange(randomBounds[0], randomBounds[1]);
            }
            if (resetStart) started = false;
        }

        public void Reset()
        {
            Reset(false);
        }

        public float Fraction()
        {
            return timepassed / TotalTime;
        }

        public bool Ping(float delta)
        {
            if (started == false)
            {
                started = true;
                if (startImmediate) return true;
            }
            timepassed += delta;
            if (timepassed >= TotalTime)
            {
                Reset();
                return true;
            }
            return false;
        }
    }
}
