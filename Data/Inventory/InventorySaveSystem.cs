using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public class InventorySaveSystem<Type>
    {
        private readonly Func<string, Type> typeLoader;

        public InventorySaveSystem(Func<string, Type> typeLoader)
        {
            this.typeLoader = typeLoader;
        }

        public InventorySaveData Save(Inventory<Type> inventory)
        {
            InventorySaveData data = new InventorySaveData();
            data.slots = new InventorySlotSaveData[inventory.SlotCount];
            for (int i = 0; i < inventory.SlotCount; i++)
            {
                InventorySlot<Type> slot = inventory.GetSlot(i);
                data.slots[i] = Save(inventory, slot);
            }
            return data;
        }

        public void Restore(Inventory<Type> inventory, InventorySaveData data)
        {
            if (data == null || data.slots == null)
            {
                return;
            }
            inventory.Contents.Clear();
            for (int i = 0; i < inventory.SlotCount && i < data.slots.Length; i++)
            {
                InventorySlotSaveData slotSave = data.slots[i];
                InventorySlot<Type> slot = inventory.GetSlot(i);
                Restore(inventory, slot, slotSave);
            }


        }

        private InventorySlotSaveData Save(Inventory<Type> inventory, InventorySlot<Type> slot)
        {
            if (slot.Count == 0 || !slot.HasResource()) return null;
            InventorySlotSaveData data = new InventorySlotSaveData();
            data.name = inventory.Contents.GenerateKey(slot.Resource);
            data.count = slot.Count;
            return data;
        }

        private void Restore(Inventory<Type> inventory, InventorySlot<Type> slot, InventorySlotSaveData data)
        {
            slot.Clear();
            if (data == null)
            {
                return;
            }
            Type result = typeLoader(data.name);
            if (result == null)
            {
                return;
            }
            slot.AddResource(result, data.count);
        }
    }
}
