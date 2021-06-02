using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public class EffectTimer
    {
        private float timeLeft;

        public EffectTimer()
        {

        }

        public void SetValue(float value)
        {
            timeLeft = value;
        }

        public bool IsActive()
        {
            return timeLeft > 0;
        }

        public float Countdown(float delta)
        {
            if (timeLeft <= 0) return 0;
            if (delta > timeLeft) delta = timeLeft;
            timeLeft -= delta;
            return delta;
        }


    }
}
