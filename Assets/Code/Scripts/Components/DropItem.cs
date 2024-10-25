using System;
using UnityEngine;

namespace IntoTheWilds
{
    [Serializable]
    public class DropItem
    {
        public GameObject ItemPrefab;
        [Range(0, 100)] public int minAmount = 1;
        [Range(0, 100)] public int maxAmount = 1;
        [Range(0f, 1f)] public float DropChance = 1f;
    }
}
