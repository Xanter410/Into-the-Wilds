using System;
using UnityEngine;

namespace IntoTheWilds.Inventory
{
    [CreateAssetMenu(fileName = "ItemData_", menuName = "Data/Items")]
    public class ItemData : ScriptableObject
    {
        public int UniqueID;

        public int MaxStack = 3;

        public GameObject DropItemPrefab;
        public Sprite InventoryIcon;
    }
}
