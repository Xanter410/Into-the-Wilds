using System.Collections.Generic;
using UnityEngine;
using Tools.Singleton;

namespace IntoTheWilds.Inventory
{
    public class ItemsDatabase : Singleton<ItemsDatabase>
    {
        [SerializeField] private ItemData[] _listOfItems;

        private readonly Dictionary<int, ItemData> _items = new();

        private void Awake()
        {
            foreach (ItemData item in _listOfItems)
            {
                _items.Add(item.UniqueID, item);
            }
        }

        public int GetMaxStackCount(int ItemID)
        {
            return GetData(ItemID).MaxStack;
        }

        public GameObject GetPrefab(int ItemID)
        {
            return GetData(ItemID).DropItemPrefab;
        }

        public Sprite GetInventoryIcon(int ItemID)
        {
            return GetData(ItemID).InventoryIcon;
        }

        private ItemData GetData(int key)
        {
            if (_items.TryGetValue(key, out ItemData data) == true)
            {
                return data;
            }
            else
            {
                Debug.LogWarning(
                    "Неудачный запрос к базе данных предметов. " +
                    "Запрошенный ID предмета: " + key);

                return null;
            }
        }
    }
}