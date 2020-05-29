using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public static class RandomExtensions
    {


        public static float NextFloatRange(this System.Random random, float min, float max)
        {
            float f = random.NextFloat();
            return MathW.Remap(f, 0, 1, min, max);
        }

        public static float NextFloat(this System.Random random)
        {
            float f = (float)random.NextDouble();
            return f;
        }
    }
}
