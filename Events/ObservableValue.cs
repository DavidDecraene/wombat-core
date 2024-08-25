using System;
namespace Wombat
{
    public class ObservableValue<Data>
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
        public event Action<Data> ValueChange;

        public ObservableValue()
        {

        }

        public ObservableValue(Data initialData)
        {
            this.IsSet = true;
            this.value = initialData;
        }

        public void SetSilent(Data value)
        {
            this.value = value;
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

        public ObservableValue<Data> UseReplay()
        {
            this.replay = true;
            return this;
        }

        public void Subscribe(Action<Data> ValueChange)
        {
            this.ValueChange += ValueChange;

            if (replay && IsSet)
            {
                ValueChange(value);
            }
        }


        public void UnSubscribe(Action<Data> ValueChange)
        {
            if (this.ValueChange != null)
                this.ValueChange -= ValueChange;
        }

        public void TriggerChange()
        {
            ValueChange?.Invoke(value);
        }
    }

}
