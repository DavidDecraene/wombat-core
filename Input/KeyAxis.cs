using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public class KeyAxis<T> where T : System.Enum
    {
        public readonly KeyConfig<T> negative;
        public readonly KeyConfig<T> positive;

        public KeyAxis(KeyConfig<T> negative, KeyConfig<T> positive)
        {
            this.negative = negative;
            this.positive = positive;
        }

        public float GetValue()
        {
            if (negative.IsHeldDown())
            {
                return -1;

            }
            else if (positive.IsHeldDown())
            {
                return 1;
            }
            return 0;
        }
    }
}
