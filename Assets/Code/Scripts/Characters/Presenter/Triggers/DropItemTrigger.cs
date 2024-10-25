using IntoTheWilds.Inventory;
using UnityEngine;

public class DropItemTrigger : MonoBehaviour
{
    private PlayerInventory _inventory;

    private void Awake()
    {
        _inventory = GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DropItems"))
        {
            DropItemInstance dropItemInstance = collision.gameObject.GetComponent<DropItemInstance>();

            if (_inventory.IsAndTakeItem(dropItemInstance.TakeItem()) == true)
            {
                dropItemInstance.DestroyItem();
            }
        }
    }
}
