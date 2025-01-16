using IntoTheWilds.Inventory;
using UnityEngine;
using VContainer;

public class DropItemTrigger : MonoBehaviour
{
    private PlayerInventory _inventory;

    private DropItemInstance _dropItemInstance;
    private bool _isStayOnItem;

    [Inject]
    public void Constuct(PlayerInventory inventory)
    {
        _inventory = inventory;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DropItems"))
        {
            _dropItemInstance = collision.gameObject.GetComponent<DropItemInstance>();

            if (_dropItemInstance.TryGetItem(out var Item) == true)
            {
                if (_inventory.IsAndTakeItem(Item) == true)
                {
                    _dropItemInstance.DestroyItem();

                    _dropItemInstance = null;
                    _isStayOnItem = false;
                }
            } 
            else
            {
                _isStayOnItem = true;
            }

        }
    }

    private void Update()
    {
        if (_isStayOnItem)
        {
            if (_dropItemInstance.TryGetItem(out var Item) == true)
            {
                if (_inventory.IsAndTakeItem(Item) == true)
                {
                    _dropItemInstance.DestroyItem();

                    _dropItemInstance = null;
                    _isStayOnItem = false;
                }
            }
        }
    }
}
