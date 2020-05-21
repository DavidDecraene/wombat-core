using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public class BitFlag
    {
        private int value;

        public int Value { get => value; }

        public void Clear()
        {
            this.value = default;
        }

        public void Of(int value)
        {
            this.value = value;
        }

        public void Set(int value)
        {
            this.value |= value;
        }

        public bool Has(int value)
        {
            return (this.value & value) == value;
        }

        public void Remove(int value)
        {
            this.value &= ~value;
        }

        public void Toggle(int value)
        {
            this.value ^= value;

        }
    }
}
