using System;
using UnityEngine;

namespace IntoTheWilds.Inventory
{
    public class PlayerInventory
    {
        public Inventory Inventory { get; private set; }

        public PlayerInventory()
        {
            Inventory = new(3);
        }

        public bool IsAndTakeItem(ItemSlot itemSlot)
        {
            if (Inventory.AddItem(itemSlot) == true)
            {
                return true;
            }

            return false;
        }

#if UNITY_EDITOR   // ----- Ниже тестовые методы ------

        public void ThrowOutSlotIndex(int slotIndex)
        {
            ItemSlot CretedItem = Inventory.RemoveSlot(slotIndex);

            Debug.Log("Выкинут предмет - " + "ID: " + CretedItem.ItemID +
                " Количество: " + CretedItem.Count);
        }

        public void InventoryToConsole()
        {
            ItemSlot slot;

            for (int slotIndex = 0; slotIndex < Inventory.Length; slotIndex++)
            {
                if ((slot = Inventory.GetSlotData(slotIndex)) != null)
                {
                    Debug.Log("ID предмета: " + slot.ItemID + " Количество: " + slot.Count);
                }
            }
        }

#endif
    }
}
