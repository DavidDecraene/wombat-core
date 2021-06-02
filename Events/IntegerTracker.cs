using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public class IntegerTracker
    {
        private int currentValue = 0;
        private bool initialized = false;
        private System.Action<int> onChange;

        public void TriggerChange()
        {
            this.onChange?.Invoke(currentValue);

        }

        public void ConfigureAction(System.Action<int> onChange)
        {
            this.onChange = onChange;
            if (initialized)
            {
                TriggerChange();
            }
        }

        public void SetValue(float value, bool checkChange = true)
        {
            SetValue(Mathf.FloorToInt(value), checkChange);
        }

        public void SetValue(int value, bool checkChange = true)
        {
            if (checkChange && value == this.currentValue)
            {
                return;
            }
            initialized = true;
            this.currentValue = value;
            TriggerChange();
        }

    }
}
