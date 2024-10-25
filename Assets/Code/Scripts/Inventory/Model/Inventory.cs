using UnityEngine;

namespace IntoTheWilds.Inventory
{
    public class Inventory
    {
        public ItemSlot[] Slots { get; private set; }

        public Inventory(int maxCountSlots)
        {
            Slots = new ItemSlot[maxCountSlots];

            for (int i = 0; i < Slots.Length; i++) 
            {
                Slots[i] = new ItemSlot();
            }
        }

        public bool AddItem(ItemSlot itemSlot)
        {
            bool isSuccessfulAdded = false;

            while (!isSuccessfulAdded) 
            {
                int slotForAdd = FindIncompleteIndexSlot(itemSlot.ItemID);

                if (slotForAdd != -1)
                {
                    Slots[slotForAdd].Add(itemSlot.ItemID, itemSlot.Count, out int excessCount);

                    _ = itemSlot.Remove(itemSlot.Count - excessCount);

                    if (excessCount <= 0)
                    {
                        isSuccessfulAdded = true;
                        break;
                    }
                }
                else
                {
                    Debug.Log("��������� ��������!");

                    return isSuccessfulAdded;
                }
            }

            return isSuccessfulAdded;
        }

        public ItemSlot RemoveItem(ItemSlot requestedItemSlot)
        {
            //int requestedCount = requestedItemSlot.Count;

            int findedItemCount = 0;
            int indexItemSlot;
            int[] findedItemIndexes = new int[Slots.Length];
            int indexFindedItems = 0;

            while (findedItemCount < requestedItemSlot.Count)
            {
                indexItemSlot = FindIndexItemSlot(requestedItemSlot.ItemID);

                if (indexItemSlot != -1)
                {
                    findedItemCount += Slots[indexItemSlot].Count;
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
                NewItemCount += Slots[findedItemIndexes[i]].Remove(requestedItemSlot.Count - NewItemCount);

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

            newItemSlot.Add(Slots[slotIndex].ItemID, Slots[slotIndex].Count, out _);

            Slots[slotIndex].Clear();

            return newItemSlot;
        }

        private int FindIndexItemSlot(int itemID)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].ItemID == itemID)
                {
                    return i;
                }
            }

            return -1;
        }

        private int FindIncompleteIndexSlot(int itemID)
        {
            int firstFreeSlot = -1;

            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].ItemID == itemID)
                {
                    if (!Slots[i].IsFull())
                    {
                        return i;
                    }
                }
                else if (firstFreeSlot == -1 && Slots[i].IsFree() == true)
                {
                    firstFreeSlot = i;
                }
            }

            return firstFreeSlot;
        }
    }
}
