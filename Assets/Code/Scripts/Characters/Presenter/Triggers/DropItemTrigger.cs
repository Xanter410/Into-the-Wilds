using IntoTheWilds.Inventory;
using IntoTheWilds.Quest;
using UnityEngine;
using VContainer;

public class DropItemTrigger : MonoBehaviour
{
    [SerializeField] private GameEventQuestProgress _questProgressEvent;
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
                TakeItem(Item);
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
                TakeItem(Item);
            }
        }
    }

    private void TakeItem(ItemSlot Item)
    {
        if (_inventory.IsAndTakeItem(Item) == true)
        {
            _questProgressEvent.TriggerEvent(
                new QuestProgressData
                {
                    ObjectiveType = ObjectiveType.Collect,
                    ResourceType = _dropItemInstance.GetResourceTypes(),
                    Amount = _dropItemInstance.GetAmount(),
                });

            _dropItemInstance.DestroyItem();

            _dropItemInstance = null;
            _isStayOnItem = false;
        }
    }
}
