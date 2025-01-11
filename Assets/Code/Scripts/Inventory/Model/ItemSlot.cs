namespace IntoTheWilds.Inventory
{
    public class ItemSlot
    {
        public int ItemID { get; private set; }
        
        public int Count { get; private set; }
        private int _maxCountStack;

        public ItemSlot()
        {
            ItemID = 0;
            Count = 0;
            _maxCountStack = 0;
        }

        public void Add(int itemID, int count, out int excessCount)
        {
            if (IsFree() == true)
            {
                ItemID = itemID;

                _maxCountStack = ItemsDatabase.Instance.GetMaxStackCount(ItemID);

                Count = 0;
            }

            int newCount = Count + count;

            if (newCount <= _maxCountStack)
            {
                Count = newCount;
                excessCount = 0;
            }
            else
            {
                Count = _maxCountStack;

                excessCount = newCount - _maxCountStack;
            }
        }

        public int Remove(int requestedCount)
        {
            int newCount = Count - requestedCount;

            if (newCount > 0)
            {
                Count = newCount;

                return requestedCount;
            } 
            else
            {
                Clear();

                return requestedCount - newCount;
            }
        }

        public void Clear()
        {
            Count = 0;
            ItemID = 0;
            _maxCountStack = 0;
        }

        public bool IsFull()
        {
            if (Count == _maxCountStack)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsFree()
        {
            if (ItemID == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
