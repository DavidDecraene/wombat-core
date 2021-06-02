using System;
namespace Wombat
{
    public class ObservableMinMaxValue
    {
        private float value = 0;
        public float Min { get; private set; }
        public float Max { get; private set; }
        public float Ratio { get => MathW.Remap(value, Min, Max, 0, 1); }
        public bool Full { get => value == Max; }
        public bool Empty { get => value == Min; }

        public float Value   // property
        {
            get { return value; }   // get method
            set
            {
                float f = UnityEngine.Mathf.Clamp(value, Min, Max);
                if (f == this.value) return;
                this.value = f;
                this.ValueChange?.Invoke(this);
            }
        }
        public event Action<ObservableMinMaxValue> ValueChange;

        public ObservableMinMaxValue(float initialData, float min, float max)
        {
            this.Min = min;
            this.Max = max;
            this.value = initialData;
        }

        public void Minimize()
        {
            Value = Min;
        }

        public void Maximize()
        {
            Value = Max;
        }

        public void Add(float delta)
        {
            Value += delta;
        }



        public void Subscribe(Action<ObservableMinMaxValue> ValueChange)
        {
            this.ValueChange += ValueChange;
            this.ValueChange?.Invoke(this);
        }


        public void UnSubscribe(Action<ObservableMinMaxValue> ValueChange)
        {
            this.ValueChange -= ValueChange;
        }
    }
}
