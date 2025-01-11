using IntoTheWilds.Inventory;
using UnityEngine;
using VContainer;

public class DropItemTrigger : MonoBehaviour
{
    private PlayerInventory _inventory;

    [Inject]
    public void Constuct(PlayerInventory inventory)
    {
        _inventory = inventory;
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
