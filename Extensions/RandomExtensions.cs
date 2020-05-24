using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public static class RandomExtensions
    {
        public static float Remap(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }


        public static float NextFloatRange(this System.Random random, float min, float max)
        {
            float f = random.NextFloat();
            return Remap(f, 0, 1, min, max);
        }

        public static float NextFloat(this System.Random random)
        {
            float f = (float)random.NextDouble();
            return f;
        }
    }
}
