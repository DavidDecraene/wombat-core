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
        private readonly Inventory<Type> parent;
        private bool lockResource = false;

        public InventorySlot(Inventory<Type> parent)
        {
            this.parent = parent;
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
            if (this.Resource == null) return 0;
            
            if (!this.Resource.Equals(resource)) { return 0; }
            int room = Count;
            if (room <= 0) return 0;
            if (amount > room) amount = room;
            SetCount(Mathf.Max(Count - amount, 0));
            parent.Contents.BalanceCount(this.Resource, -amount);
            if (Count <= 0 && !lockResource)
            {
                this.Resource = default;
                this.OnSlotEvent?.Invoke(this);
            }
            return amount;
        }

        public int AcceptResource(Type resource, int amount)
        {
            if (amount <= 0) return 0;
            if (this.Resource == null) return this.parent.GetStackSize(resource);
            if (!this.Resource.Equals(resource)) { return 0; }

            int room = this.parent.GetStackSize(resource) - Count;
            if (room <= 0) return 0;
            if (amount > room) amount = room;
            return amount;
        }

        public int AddResource(Type resource, int amount)
        {
            if (amount <= 0) return 0;
            if (this.Resource != null)
            {
                if (!this.Resource.Equals(resource)) { return 0; }
            }
            else
            {
                this.Resource = resource;
                this.OnSlotEvent?.Invoke(this);
            }
            int room = this.parent.GetStackSize(resource) - Count;
            if (room <= 0) return 0;
            if (amount > room) amount = room;
            SetCount(Count + amount);
            parent.Contents.BalanceCount(this.Resource, amount);
            return amount;
        }
    }
}
