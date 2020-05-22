using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public class Clock
    {
        private bool clockRunning = false;
        public int days;
        public int hours;
        public int minutes;
        public int seconds;
        private float timeRest;
        public float timeSpeed = 30; // 60.0f * 15; // 30; // 30 60.0f*15; 

        public string Render()
        {
            string result = string.Format("D {0} H {1} M {2} ", days, hours, minutes);
            return result;
        }

        public float DayFraction()
        {
            float f = seconds + (minutes * 60) + (hours * 3600);
            return f / 86400f;
        }

        public void Pause()
        {
            clockRunning = false;
        }

        public bool IsActive()
        {
            return clockRunning;
        }

        public void Start()
        {
            clockRunning = true;
        }

        void UpdateMinutes(int add)
        {
            minutes += add;
            if (minutes > 59)
            {
                int iRest = minutes % 60;
                UpdateHours((minutes - iRest) / 60);
                minutes = iRest;
            }
        }

        void UpdateHours(int add)
        {
            hours += add;
            if (hours > 23)
            {
                int iRest = hours % 24;
                UpdateDays((hours - iRest) / 24);
                hours = iRest;
            }
        }

        void UpdateDays(int add)
        {
            days += add;

        }

        void UpdateSeconds(int add)
        {
            seconds += add;
            if (seconds > 59)
            {

                int iRest = seconds % 60;
                UpdateMinutes((seconds - iRest) / 60);
                seconds = iRest;
            }

        }

        public bool Update(float deltaTime)
        {

            if (!clockRunning) { return false; }
            timeRest += Time.deltaTime * timeSpeed;
            bool clockChanged = false;
            if (timeRest > 60 * 60 * 24)
            {
                clockChanged = true;
                float rest = timeRest % (60 * 60 * 24);
                days += Mathf.FloorToInt((timeRest - rest) / (60 * 60 * 24));
                timeRest = rest;
            }
            if (timeRest > 60 * 60)
            {
                clockChanged = true;
                float rest = timeRest % (60 * 60);
                UpdateHours(Mathf.FloorToInt((timeRest - rest) / (60 * 60)));
                timeRest = rest;
            }
            if (timeRest > 60)
            {
                clockChanged = true;
                float rest = timeRest % 60;
                UpdateMinutes(Mathf.FloorToInt((timeRest - rest) / 60));
                timeRest = rest;
            }
            if (timeRest >= 1)
            {
                clockChanged = true;
                float rest = timeRest % 1;
                UpdateSeconds(Mathf.FloorToInt(timeRest - rest));
                timeRest = rest;
            }
            return clockChanged;
        }
    }
}
