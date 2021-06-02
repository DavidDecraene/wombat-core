using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public class Clock
    {
        private bool clockRunning = false;
        public int hoursInDay = 24;
        public int month;
        public int year;
        public int day;
        public int hour;
        public int minute;
        public int second;
        private float timeRest;
        public float timeSpeed = 30; // 60.0f * 15; // 30; // 30 60.0f*15; 

        public string Render()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder( 50);
            if (year > 0)
            {
                sb.Append(year).Append(year > 1 ? " years " : "year ");
            }
            if (month > 0)
            {
                sb.Append(month).Append(month > 1 ? " months " : "month ");
            }
            if (day > 0)
            {
                sb.Append(day).Append(day > 1 ? " days " : "day ");
            }
            if (hour > 0)
            {
                sb.Append(hour).Append(hour > 1 ? " hours " : "hour ");
            }
            if (minute > 0)
            {
                sb.Append(minute).Append(hour > 1 ? " minutes " : "minute ");
            }
            if (second > 0)
            {
                sb.Append(second).Append(second > 1 ? " seconds " : "second ");

            }
            return sb.ToString();
        }

        public float DayFraction()
        {
            float ht = 60 * 60;
            float dt = ht * hoursInDay;
            float f = second + (minute * 60) + (hour * ht);
            return f / dt;
        }

        public float Hours()
        {
            return hour + minute / 60f;
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
            minute += add;
            if (minute >= 60)
            {
                int iRest = minute % 60;
                UpdateHours((minute - iRest) / 60);
                minute = iRest;
            }
        }

        void UpdateHours(int add)
        {
            hour += add;
            if (hour >= hoursInDay)
            {
                int iRest = hour % hoursInDay;
                UpdateDays((hour - iRest) / hoursInDay);
                hour = iRest;
            }
        }

        void UpdateDays(int add)
        {
            day += add;

        }

        void UpdateSeconds(int add)
        {
            second += add;
            if (second >= 60)
            {

                int iRest = second % 60;
                UpdateMinutes((second - iRest) / 60);
                second = iRest;
            }

        }

        public bool Update(float deltaTime)
        {

            if (!clockRunning) { return false; }
            timeRest += deltaTime * timeSpeed;
            bool clockChanged = false;
            float hours = 60 * 60;
            float days = hours * hoursInDay;
            if (timeRest > days)
            {
                clockChanged = true;
                float rest = timeRest % (days);
                day += Mathf.FloorToInt((timeRest - rest) / (days));
                timeRest = rest;
            }
            if (timeRest > hours)
            {
                clockChanged = true;
                float rest = timeRest % (hours);
                UpdateHours(Mathf.FloorToInt((timeRest - rest) / (hours)));
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
