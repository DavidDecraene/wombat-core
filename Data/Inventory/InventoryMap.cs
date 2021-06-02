using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wombat
{
    public interface INameReference
    {
        string GetName();
    }

    public struct TypeQuantity<Type>
    {
        public int count;
        public readonly Type value;

        public TypeQuantity(Type value, int count)
        {
            this.count = count;
            this.value = value;
        }
    }

    public class InventoryMap<Type>
    {
        private Dictionary<Type, TypeQuantity<Type>> values = new Dictionary<Type, TypeQuantity<Type>>();
        public int Count { get => values.Count;  }

        public event Action<TypeQuantity<Type>> OnInventoryChange;

        public InventoryMap()
        {
        }

        public void Clear()
        {
            values.Clear();
        }

        public void AddTo(InventoryMap<Type> other)
        {
            foreach(var entry in values)
            {
                other.BalanceCount(entry.Key, entry.Value.count);
            }
        }

        public int GetCount(Type resource)
        {
            if (resource == null) return 0;
            if (values.TryGetValue(resource, out TypeQuantity<Type> currentValue))
            {
                return currentValue.count;
            }
            return 0;
        }

        public IEnumerable<Type> GetKeys()
        {
            return values.Keys;
        }

        public IEnumerable<TypeQuantity<Type>> GetContents()
        {
            return values.Values.ToList();
        }

        public Type[] Keys()
        {
            Type[] result = new Type[values.Count];
            int i = 0;
            foreach (TypeQuantity<Type> k in values.Values)
            {
                result[i++] = k.value;
            }
            return result;
        }


        public int AddResource(Type resource, int amount, int max)
        {
            if (resource == null) return 0;
            if (amount <= 0) return 0;
            int room = max - Mathf.CeilToInt(GetCount(resource));
            if (room <= 0) return 0;
            if (amount > room) amount = room;
            BalanceCount(resource, amount);
            return amount;
        }

        public int AcceptResource(Type resource, int amount, int max)
        {
            if (resource == null) return 0;
            if (amount <= 0) return 0;

            int room = max - Mathf.CeilToInt(GetCount(resource));
            if (room <= 0) return 0;
            if (amount > room) amount = room;
            return amount;
        }

        public int RemoveResource(Type resource, int amount)
        {
            if (resource == null) return 0;
            if (amount <= 0) return 0;
            int room = Mathf.FloorToInt(GetCount(resource));
            if (room <= 0) return 0;
            if (amount > room) amount = room;
            BalanceCount(resource, -amount);
            return amount;
        }


        public void BalanceCount(Type resource, int amount)
        {
            if (amount == 0) return;
            if (resource == null) return;
            if (values.TryGetValue(resource, out TypeQuantity<Type> currentValue))
            {
                currentValue.count += amount;
                if (currentValue.count <= 0)
                {
                    // remove it
                    values.Remove(resource);
                    this.OnInventoryChange?.Invoke(currentValue);
                    return;
                }
                values[resource] = currentValue;
                this.OnInventoryChange?.Invoke(currentValue);
            }
            else
            {
                if (amount < 0) return;
                TypeQuantity<Type> mapData = new TypeQuantity<Type>(resource, amount);
                values.Add(resource, mapData);
                this.OnInventoryChange?.Invoke(mapData);
                return;
            }
        }
    }
}
