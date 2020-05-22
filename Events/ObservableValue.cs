using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{    
    public class ObservableValue<Data>
    {
        private Data value = default;
        private bool isSet = false;
        private bool replay = false;

        public Data Value   // property
        {
            get { return value; }   // get method
            set
            {
                if (value == null)
                {
                    if (this.value == null) return;
                } else if (System.Object.ReferenceEquals(value, this.value))
                {
                     return;
                }
                this.isSet = true;
                this.value = value;
                this.Next(value);
            }
        }
        public event Action<Data> ValueChange;

        public ObservableValue()
        {

        }

        public ObservableValue(Data initialData)
        {
            this.isSet = true;
            this.value = initialData;
        }

        public ObservableValue<Data> UseReplay()
        {
            this.replay = true;
            return this;
        }

        public void Subscribe(Action<Data> ValueChange)
        {
            this.ValueChange += ValueChange;

            if (replay && isSet && value != null)
            {
                ValueChange(value);
            }
        }


        public void UnSubscribe(Action<Data> ValueChange)
        {
            this.ValueChange -= ValueChange;
        }

        public void Next(Data data)
        {
            this.ValueChange?.Invoke(data);
        }
    }
}
