using System;

namespace IntoTheWilds.Inventory
{
    public class Inventory
    {
        public int Length { get; private set; }

        public event Action<int> SlotChanged;

        private readonly ItemSlot[] _slots;

        public Inventory(int maxCountSlots)
        {
            _slots = new ItemSlot[maxCountSlots];

            Length = maxCountSlots - 1;

            for (int i = 0; i < _slots.Length; i++) 
            {
                _slots[i] = new ItemSlot();
            }
        }

        public ItemSlot GetSlotData(int slotID)
        {
            if (slotID >= 0 && slotID < _slots.Length)
            {
                return _slots[slotID];
            }

            return null;
        }

        public bool AddItem(ItemSlot itemSlot)
        {
            bool isSuccessfulAdded = false;

            while (!isSuccessfulAdded) 
            {
                int slotForAdd = FindIncompleteIndexSlot(itemSlot.ItemID);

                if (slotForAdd != -1)
                {
                    _slots[slotForAdd].Add(itemSlot.ItemID, itemSlot.Count, out int excessCount);

                    SlotChanged?.Invoke(slotForAdd);

                    _ = itemSlot.Remove(itemSlot.Count - excessCount);

                    if (excessCount <= 0)
                    {
                        isSuccessfulAdded = true;
                        break;
                    }
                }
                else
                {
                    return isSuccessfulAdded;
                }
            }

            return isSuccessfulAdded;
        }

        public void AddSlot(int slotIndex, ItemSlot itemSlot)
        {
            _slots[slotIndex].Clear();
            _slots[slotIndex].Add(itemSlot.ItemID, itemSlot.Count, out _);
            SlotChanged?.Invoke(slotIndex);
        }

        public ItemSlot RemoveItem(ItemSlot requestedItemSlot)
        {
            //int requestedCount = requestedItemSlot.Count;

            int findedItemCount = 0;
            int indexItemSlot;
            int[] findedItemIndexes = new int[_slots.Length];
            int indexFindedItems = 0;

            while (findedItemCount < requestedItemSlot.Count)
            {
                indexItemSlot = FindIndexItemSlot(requestedItemSlot.ItemID);

                if (indexItemSlot != -1)
                {
                    findedItemCount += _slots[indexItemSlot].Count;
                    findedItemIndexes[indexFindedItems] = indexItemSlot;
                    indexFindedItems++;
                }
                else
                {
                    return null;
                }
            }

            int NewItemCount = 0;

            for (int i = 0; i < findedItemIndexes.Length; i++) 
            {
                NewItemCount += _slots[findedItemIndexes[i]].Remove(requestedItemSlot.Count - NewItemCount);

                SlotChanged?.Invoke(findedItemIndexes[i]);

                if (NewItemCount == requestedItemSlot.Count)
                {
                    break;
                }
            }

            ItemSlot newItemSlot = new();

            newItemSlot.Add(requestedItemSlot.ItemID, NewItemCount, out _);

            return newItemSlot;
        }

        public ItemSlot RemoveSlot(int slotIndex)
        {
            ItemSlot newItemSlot = new();

            newItemSlot.Add(_slots[slotIndex].ItemID, _slots[slotIndex].Count, out _);

            _slots[slotIndex].Clear();

            SlotChanged?.Invoke(slotIndex);

            return newItemSlot;
        }

        private int FindIndexItemSlot(int itemID)
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i].ItemID == itemID)
                {
                    return i;
                }
            }

            return -1;
        }

        private int FindIncompleteIndexSlot(int itemID)
        {
            int firstFreeSlot = -1;

            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i].ItemID == itemID)
                {
                    if (!_slots[i].IsFull())
                    {
                        return i;
                    }
                }
                else if (firstFreeSlot == -1 && _slots[i].IsFree() == true)
                {
                    firstFreeSlot = i;
                }
            }

            return firstFreeSlot;
        }
    }
}

