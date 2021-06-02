using System.Collections;
using System.Collections.Generic;
namespace Wombat
{

    public class MovingAverage
    {
        private Queue<float> queue = new Queue<float>();
        public readonly int size;

        public float Average { get; private set; } = 0;
        public int Count { get => queue.Count; }

        public MovingAverage(int size)
        {
            this.size = size;
        }

        public void Clear()
        {
            queue.Clear();
            Average = 0;
        }

        public void Add(float value)
        {
            if (queue.Count >= size)
            {
                RemoveEntry();
            }
            AddEntry(value);
        }

        private void AddEntry(float value)
        {
            queue.Enqueue(value);
            int count = queue.Count;
            Average = Average + (value - Average) / count;
        }

        private void RemoveEntry()
        {
            int count = queue.Count;
            if (count == 0) return;
            float value = queue.Dequeue();
            count--;
            if (count == 0)
            {
                Average = 0;
                return;
            }
            Average = ((Average * (count + 1)) - value) / count;
        }

    }
}
