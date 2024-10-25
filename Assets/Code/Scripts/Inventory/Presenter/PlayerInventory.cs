using UnityEngine;

namespace IntoTheWilds.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        private Inventory _inventory;

        private void Awake()
        {
            _inventory = new(3);
        }

        public bool IsAndTakeItem(ItemSlot itemSlot)
        {
            if (_inventory.AddItem(itemSlot) == true)
            {
                return true;
            }

            return false;
        }



        // ----- Ниже тестовые методы ------

        public void ThrowOutSlot(int slotIndex)
        {
            _inventory.RemoveSlot(slotIndex);
        }

        public void ThrowOutInt(int slotIndex)
        {
            ItemSlot requestedItem = new ItemSlot();

            requestedItem.Add(
                _inventory.Slots[slotIndex].ItemID,
                _inventory.Slots[slotIndex].Count,
                out _);

            ThrowOut(requestedItem);
        }

        public void ThrowOut(ItemSlot itemSlot)
        {
            ItemSlot CretedItem = _inventory.RemoveItem(itemSlot);

            Debug.Log("Выкинут предмет - " + "ID: " + CretedItem.ItemID +
                " Количество: " + CretedItem.Count);
        }

#if UNITY_EDITOR

        public void InventoryToConsole()
        {
            foreach (var item in _inventory.Slots)
            {
                if (item != null)
                {
                    Debug.Log("ID предмета: " + item.ItemID + " Количество: " + item.Count);
                }
            }
        }

#endif
    }
}
