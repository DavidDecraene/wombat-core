using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public interface INameReference
    {
        string GetName();
    }

    public struct InventoryMapData<Type>
    {
        public int count;
        public readonly Type value;

        public InventoryMapData(Type value, int count)
        {
            this.count = count;
            this.value = value;
        }
    }

    public class InventoryMap<Type>
    {
        private Dictionary<string, InventoryMapData<Type>> values = new Dictionary<string, InventoryMapData<Type>>();
        private readonly Func<Type, string> keyGen;

        public event Action<InventoryMapData<Type>> OnInventoryChange;

        public InventoryMap(Func<Type, string> keyGen)
        {
            this.keyGen = keyGen;
        }

        public string GenerateKey(Type resource)
        {
            if (resource == null) return null;
            return keyGen(resource);
        }

        public int GetCount(Type resource)
        {
            if (resource == null) return 0;
            if (values.TryGetValue(keyGen(resource), out InventoryMapData<Type> currentValue))
            {
                return currentValue.count;
            }
            return 0;
        }

        public Type[] Keys()
        {
            Type[] result = new Type[values.Count];
            int i = 0;
            foreach (InventoryMapData<Type> k in values.Values)
            {
                result[i++] = k.value;
            }
            return result;
        }

        public void Clear()
        {
            values.Clear();
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
            string key = keyGen(resource);
            if (values.TryGetValue(key, out InventoryMapData<Type> currentValue))
            {
                currentValue.count += amount;
                if (currentValue.count <= 0)
                {
                    // remove it
                    values.Remove(key);
                    this.OnInventoryChange?.Invoke(currentValue);
                    return;
                }
                values[key] = currentValue;
                this.OnInventoryChange?.Invoke(currentValue);
            }
            else
            {
                if (amount < 0) return;
                InventoryMapData<Type> mapData = new InventoryMapData<Type>(resource, amount);
                values.Add(key, mapData);
                this.OnInventoryChange?.Invoke(mapData);
                return;
            }
        }
    }
}
