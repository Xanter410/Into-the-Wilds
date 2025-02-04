using UnityEngine;

namespace IntoTheWilds.Inventory
{
    public class PlayerInventory
    {
        public Inventory Inventory { get; private set; }
        private readonly Rigidbody2D _playerRigidbody2D;

        public PlayerInventory(Rigidbody2D playerRigidbody2D)
        {
            Inventory = new(4);
            _playerRigidbody2D = playerRigidbody2D;
        }

        public bool IsAndTakeItem(ItemSlot itemSlot)
        {
            if (Inventory.AddItem(itemSlot) == true)
            {
                return true;
            }

            return false;
        }

        public void RemoveSlot(int slotIndex)
        {
            _ = Inventory.RemoveSlot(slotIndex);
        }

        public void ThrowAwayItem(ItemSlot dropItemModel)
        {
            GameObject dropItemGameObject = ItemsDatabase.Instance.GetPrefab(dropItemModel.ItemID);
            Vector2 spawnPosition = _playerRigidbody2D.position + Vector2.down;

            GameObject dropItemInstance = GameObject.Instantiate(dropItemGameObject, spawnPosition, Quaternion.identity);

            dropItemInstance.GetComponent<DropItemInstance>().SetCount(dropItemModel.Count);
        }

        public void AddItem(ItemSlot itemSlot)
        {
            _ = Inventory.AddItem(itemSlot);
        }
    }
}
