using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public class InventorySlot<Type>
    {

        public event Action<InventorySlot<Type>> OnSlotEvent;
        public event Action<InventorySlot<Type>, int> OnContentChangeEvent;

        public Type Resource { get; private set; }
        public int Count { get; private set;  }
        public Inventory<Type> Parent { get; private set; }
        private bool lockResource = false;
        public int StackMaximum { get; set; } = -1;

        public Predicate<Type> filter;

        public InventorySlot(Inventory<Type> parent)
        {
            this.Parent = parent;
        }

        public void LockResource(Type resource)
        {
            if (resource == null) return;
            this.lockResource = true;
            if (resource.Equals(this.Resource)) return;
            SetCount(0);
            this.Resource = resource;

        }

        private void SetCount(int ct)
        {
            if (ct == this.Count) return;
            this.Count = ct;
            this.OnContentChangeEvent?.Invoke(this, Count);
        }

        public bool HasResource()
        {
            return Resource != null;
        }

        public void Clear()
        {
            if (Count > 0 && Resource != null)
            {
                SetCount(0);
                if (!lockResource)
                {
                    this.Resource = default;
                    this.OnSlotEvent?.Invoke(this);
                }
            }
            else
            {
                // wipe anyway
                SetCount(0);
                this.Resource = default;
            }
        }

        public int RemoveResource(Type resource, int amount)
        {
            if (amount <= 0) return 0;
            if (resource == null) return 0;
            if (this.Resource == null) return 0;
            
            if (!this.Resource.Equals(resource)) { return 0; }
            int room = Count;
            if (room <= 0) return 0;
            if (amount > room) amount = room;
            SetCount(Mathf.Max(Count - amount, 0));
            Parent.Contents.BalanceCount(this.Resource, -amount);
            if (Count <= 0 && !lockResource)
            {
                this.Resource = default;
                this.OnSlotEvent?.Invoke(this);
            }
            return amount;
        }

        public int GetStackSize(Type resource, bool checkLocalLimits = true)
        {
            int size = Parent.GetStackSize(resource);
            if (checkLocalLimits && StackMaximum > 0 && StackMaximum < size) size = StackMaximum;
            return size;
        }

        public int AcceptResource(Type resource, int amount)
        {
            if (amount <= 0) return 0;
            if (resource == null) return 0;
            if (filter != null && !filter(resource)) return 0;
            if (this.Resource == null) return Mathf.Min(amount, GetStackSize(resource));
            if (!this.Resource.Equals(resource)) { return 0; }

            int room = GetStackSize(resource) - Count;
            if (room <= 0) return 0;
            return Mathf.Min(amount, room);
        }

        public int RestoreResource(Type resource, int amount)
        {
            if (amount <= 0) return 0;
            if (resource == null) return 0;
            if (filter != null && !filter(resource)) return 0;
            if (Resource != null)
            {
                if (!Resource.Equals(resource)) { return 0; }
            }
            else
            {
                this.Resource = resource;
                this.OnSlotEvent?.Invoke(this);
            }
            int room = GetStackSize(resource, false) - Count;
            if (room <= 0) return 0;
            if (amount > room) amount = room;
            SetCount(Count + amount);
            Parent.Contents.BalanceCount(Resource, amount);
            return amount;
        }

        public int AddResource(Type resource, int amount)
        {
            if (amount <= 0) return 0;
            if (resource == null) return 0;
            if (filter != null && !filter(resource)) return 0;
            if (Resource != null)
            {
                if (!Resource.Equals(resource)) { return 0; }
            }
            else
            {
                this.Resource = resource;
                this.OnSlotEvent?.Invoke(this);
            }
            int room = GetStackSize(resource) - Count;
            if (room <= 0) return 0;
            if (amount > room) amount = room;
            SetCount(Count + amount);
            Parent.Contents.BalanceCount(Resource, amount);
            return amount;
        }

    }
}
