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

        public Inventory(int size, Func<Type, int> stackSize)
        {
            this.stackSize = stackSize;
            Contents = new InventoryMap<Type>();
            for (int i = 0; i < size; i++) slots.Add(new InventorySlot<Type>(this));

        }

        public Inventory(int size, Func<Type, int> stackSize, Predicate<Type> filter)
        {
            this.stackSize = stackSize;
            Contents = new InventoryMap<Type>();
            for (int i = 0; i < size; i++)
            {
                InventorySlot<Type> slot = new InventorySlot<Type>(this);
                slot.filter = filter;
                slots.Add(slot);
            }

        }

        public void Sort()
        {
            InventoryMap<Type> copyMap = new InventoryMap<Type>();
            //Inventory<Type> sortInv = new Inventory<Type>(slots.Count, stackSize);
            foreach(InventorySlot<Type> slot in slots)
            {
                if (slot == null) continue;
                if (slot.Count <= 0) continue;
                copyMap.AddResource(slot.Resource, slot.Count, int.MaxValue);
                //sortInv.AddResource(slot.Resource, slot.Count);
            }
            Clear();
            SortedSet<Type> sortedSet = new SortedSet<Type>(copyMap.Keys());
            // Could just use a map as well.
            // Could sort alphabetically. Consider.
            foreach(Type t in sortedSet)
            {
                int ct = copyMap.GetCount(t);
                AddResource(t, ct);
            }
            /**
            foreach (InventorySlot<Type> slot in sortInv.slots)
            {
                if (slot == null) continue;
                if (slot.Count <= 0) continue;
                AddResource(slot.Resource, slot.Count);
            } */

        }

        public int GetStackSize(Type definition)
        {
            if (definition == null) return 0;
            return stackSize(definition);
        }

        public int GetCount(Type resource)
        {
            return Contents.GetCount(resource);
        }

        public void Clear()
        {

            foreach (InventorySlot<Type> s in slots)
            {
                s.Clear();
            }
            Contents.Clear();
        }

        public IEnumerable<InventorySlot<Type>> GetSlots()
        {
            return slots;
        }

        public InventorySlot<Type> GetSlot(int index)
        {
            if (index < 0 || index >= this.slots.Count) return null;
            return this.slots[index];
        }

        public int RestoreResource(int slotIndex, Type resource, int amount)
        {
            InventorySlot<Type> slot = GetSlot(slotIndex);
            if (slot == null) return 0;
            return slot.RestoreResource(resource, amount);
        }

        public int AddResource(TypeQuantity<Type> resource)
        {
            return AddResource(resource.value, resource.count);
        }

        public bool AddResource(Type resource)
        {
            return AddResource(resource, 1) == 1;
        }

        public int AcceptResource(TypeQuantity<Type> resource)
        {
            return AcceptResource(resource.value, resource.count);
        }

        public int AcceptResource(Type resource, int amount)
        {
            if (resource == null) return 0;
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
            if (resource == null) return 0;
            int total = 0;
            // First add to existing slots...
            foreach (InventorySlot<Type> s in slots)
            {
                if (s.Resource != null)
                {
                    int added = s.AddResource(resource, amount);
                    if (added > 0)
                    {
                        amount -= added;
                        total += added;
                    }
                    if (amount <= 0) return total;
                    continue;
                }
            }
            // Then fill empty slots..
            if (amount <= 0) return total;
            foreach (InventorySlot< Type> s in slots)
            {
                if (s.Resource == null)
                {
                    int added = s.AddResource(resource, amount);
                    if (added > 0)
                    {
                        amount -= added;
                        total += added;
                    }
                    if (amount <= 0) return total;

                }
            }
            return total;
        }

        public bool RemoveResource(Type resource)
        {
            return RemoveResource(resource, 1) == 1;
        }



        public int RemoveResource(TypeQuantity<Type> resource)
        {
            return RemoveResource(resource.value, resource.count);
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
