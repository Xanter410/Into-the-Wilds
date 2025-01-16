using UnityEngine;

namespace IntoTheWilds.Inventory
{
    public class DropItemInstance : MonoBehaviour
    {
        [SerializeField] private ItemData _itemData;
        [SerializeField] private int _itemCount = 1;

        private ItemSlot _slot;

        private void Start()
        {
            _slot = new();

            _slot.Add(_itemData.UniqueID, _itemCount, out _);
        }

        public void SetCount(int count)
        {
            _itemCount = count;
        }

        public ItemSlot TakeItem()
        {
            return _slot;
        }

        public void DestroyItem()
        {
            Destroy(gameObject);
        }
    }
}