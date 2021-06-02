using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public class ValueTracker<Data>
    {
        private Data value = default;
        public bool IsSet { get; private set; } = false;
        private bool replay = false;

        public Data Value   // property
        {
            get { return value; }   // get method
            set
            {
                SetValue(value);
            }
        }
        private System.Action<Data> ValueChange;

        public ValueTracker()
        {

        }

        public ValueTracker(Data initialData)
        {
            this.IsSet = true;
            this.value = initialData;
        }

        public void SetValue(Data value, bool checkChange = true)
        {
            if (checkChange && Equals(value, this.value))
            {
                return;
            }
            IsSet = true;
            this.value = value;
            TriggerChange();
        }

        public ValueTracker<Data> UseReplay()
        {
            this.replay = true;
            return this;
        }

        public void ConfigureAction(System.Action<Data> ValueChange)
        {
            this.ValueChange = ValueChange;
            if (ValueChange == null) return;

            if (replay && IsSet)
            {
                ValueChange(value);
            }
        }

        public void TriggerChange()
        {
            ValueChange?.Invoke(value);
        }

    }
}
