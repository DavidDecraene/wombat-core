using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public class Inventory<Type>
    {
        private readonly List<InventorySlot<Type>> slots = new List<InventorySlot<Type>>();
        public InventoryMap<Type> Contents { get; private set; }
        private readonly Func<Type, int> stackSize;
        public int SlotCount { get => slots.Count; }

        public Inventory(int size, Func<Type, string> keyGen, Func<Type, int> stackSize)
        {
            this.stackSize = stackSize;
            Contents = new InventoryMap<Type>(keyGen);
            for (int i = 0; i < size; i++) slots.Add(new InventorySlot<Type>(this));

        }

        public int GetStackSize(Type definition)
        {
            if (definition == null) return 0;
            return stackSize(definition);
        }

        /**
        public void Restore(InventorySaveData data)
        {
            if (data == null || data.slots == null)
            {
                return;
            }
            contents.Clear();
            for (int i = 0; i < slots.Count && i < data.slots.Length; i++)
            {
                InventorySlotSaveData slotSave = data.slots[i];
                InventorySlot slot = slots[i];
                slot.Restore(slotSave);
                contents.BalanceCount(slot.GetResource(), slot.GetResourceCount());
            }


        }*/

        public IEnumerable<InventorySlot<Type>> GetSlots()
        {
            return slots;
        }

        public InventorySlot<Type> GetSlot(int index)
        {
            if (index < 0 || index >= this.slots.Count) return null;
            return this.slots[index];
        }

        public bool AddResource(Type resource)
        {
            return AddResource(resource, 1) == 1;
        }

        public int AcceptResource(Type resource, int amount)
        {
            int asked = amount;
            int total = 0;
            foreach (InventorySlot< Type> s in slots)
            {
                int added = s.AcceptResource(resource, amount);
                if (added > 0)
                {
                    asked -= added;
                    total += added;
                }
                if (asked <= 0) { return amount; }
            }
            return Mathf.Min(total, amount);

        }

        public int AddResource(Type resource, int amount)
        {
            int total = 0;
            foreach (InventorySlot< Type> s in slots)
            {
                int added = s.AddResource(resource, amount);
                if (added > 0)
                {
                    amount -= added;
                    total += added;
                }
                if (amount <= 0) break;
            }
            return total;
        }

        public bool RemoveResource(Type resource)
        {
            return RemoveResource(resource, 1) == 1;
        }

        public int RemoveResource(Type resource, int amount)
        {
            int total = 0;
            foreach (InventorySlot< Type> s in slots)
            {
                int added = s.RemoveResource(resource, amount);
                if (added > 0)
                {
                    amount -= added;
                    total += added;
                }
                if (amount <= 0) break;
            }
            return total;
        }
    }
}
