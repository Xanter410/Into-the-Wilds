using UnityEngine;

namespace IntoTheWilds.Inventory
{
    public class DropItemInstance : MonoBehaviour
    {
        [SerializeField] private ItemData _itemData;
        [SerializeField] private ResourceType _resourceTypes;
        [SerializeField] private int _itemCount = 1;
        [SerializeField] private float _timeIgnoreModeBeforeSpawn = 0.6f;

        private ItemSlot _slot;

        private void Start()
        {
            _slot = new();

            _slot.Add(_itemData.UniqueID, _itemCount, out _);
        }

        private void Update()
        {
            _timeIgnoreModeBeforeSpawn -= Time.deltaTime;
        }

        public int GetAmount()
        {
            return _itemCount;
        }

        public ResourceType GetResourceTypes()
        { 
            return _resourceTypes; 
        }

        public void SetCount(int count)
        {
            _itemCount = count;
        }

        public bool TryGetItem(out ItemSlot item)
        {
            if (_timeIgnoreModeBeforeSpawn <= 0)
            {
                item = _slot;
                return true;
            }

            item = null;
            return false;
        }

        public void DestroyItem()
        {
            Destroy(gameObject);
        }
    }
}