using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public class MultiMap<Key, Value>
    {
        private Dictionary<Key, List<Value>> values = new Dictionary<Key, List<Value>>();
        private readonly bool removeEmptyLists = true;

        public IEnumerable<Key> Keys { get => values.Keys; }

        public Value Find(Key key, Predicate<Value> filter)
        {
            if (key == null) return default;
            if (!values.ContainsKey(key))
            {
                return default;
            }
            List<Value> existing = values[key];
            if (existing == null) return default;
            foreach(Value v in existing)
            {
                if (filter(v)) return v;
            }
            return default;
        }

        public bool ContainsKey(Key key)
        {
            return values.ContainsKey(key);
        }

        public void Add(Key key, Value value)
        {
            if (key == null || value == null) return;
            if(!values.TryGetValue(key, out List<Value> existing)){
                values[key] = existing = new List<Value>();
            }
            existing.Add(value);
        }

        public void AddAll(Key key, IEnumerable<Value> value)
        {
            if (key == null || value == null) return;

            List<Value> existing = null;
            foreach (Value v in value)
            {
                if (existing == null)
                {
                    if (!values.TryGetValue(key, out existing))
                    {
                        values[key] = existing = new List<Value>();
                    }
                }
                existing.Add(v);
            }
            
        }


        public void Clear()
        {
            values.Clear();
        }

        public bool IsEmpty()
        {
            return values.Count == 0;
        }

        public int Count(Key key)
        {
            if (key == null) return 0;
            if (values.TryGetValue(key, out List<Value> existing))
            {
                return existing.Count;
            }
            return 0;

        }

        public IEnumerable<Value> Values(Key key)
        {
            if (key == null) yield break;
            if (values.TryGetValue(key, out List<Value> existing))
            {
                foreach (Value value in existing) yield return value;
            }

        }

        public bool Remove(Key key, Value value)
        {
            if (key == null || value == null) return false;
            if (!values.TryGetValue(key, out List<Value> existing))
            {
                return false;
            }
            bool removed = existing.Remove(value);
            if (removed && removeEmptyLists && existing.Count == 0)
            {
                values.Remove(key);
            }
            return removed;
        }

        public bool Remove(Key key)
        {
            if (key == null) return false;
            return values.Remove(key);
        }

    }
}
