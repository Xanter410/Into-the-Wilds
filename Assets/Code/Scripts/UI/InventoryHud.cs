using IntoTheWilds.Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

public class InventoryHud : MonoBehaviour
{
    private struct SlotHUD
    {
        public int ID;
        public int ItemID;
        public Label Count;
        public VisualElement Icon;
    }

    private PlayerInventory _playerInventory;
    private UIDocument _uidocument;

    private Dictionary<int, SlotHUD> _inventoryHud = new Dictionary<int, SlotHUD>();

    [Inject]
    public void Constuct(PlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;
    }

    private void OnEnable()
    {
        _uidocument = GetComponent<UIDocument>();

        _playerInventory.Inventory.SlotChanged += InventorySlotChanged;

        for (int slotID = 0; slotID <= _playerInventory.Inventory.Length; slotID++)
        {
            ItemSlot slot = _playerInventory.Inventory.GetSlotData(slotID);

            _inventoryHud.Add(slotID, new SlotHUD
            {
                ID = slotID,
                ItemID = slot.ItemID,
                Count = _uidocument.rootVisualElement.Q<VisualElement>("Slot_" + slotID.ToString()).Q<Label>("ItemCounter_" + slotID.ToString() + "_1"),
                Icon = _uidocument.rootVisualElement.Q<VisualElement>("Slot_" + slotID.ToString()).Q<VisualElement>("ItemIcon_" + slotID.ToString() + "_0")
            });
        }
    }

    private void OnDisable()
    {
        _playerInventory.Inventory.SlotChanged -= InventorySlotChanged;
    }

    private void InventorySlotChanged(int SlotID)
    {
        ItemSlot slot = _playerInventory.Inventory.GetSlotData(SlotID);
        if (slot == null)
        {
            Debug.LogWarning("Error: slot не найден в инвентаре PlayerInventory. SlotID = " + SlotID);
        }

        if (_inventoryHud.TryGetValue(SlotID, out var hud) == true)
        {
            hud.Count.text = slot.Count.ToString();
            var sprite = ItemsDatabase.Instance.GetInventoryIcon(slot.ItemID);
            hud.Icon.style.backgroundImage = new StyleBackground(sprite);
        }
        else
        {
            Debug.LogWarning("Error: slot не найден в словаре InventoryHUD. SlotID = " + SlotID);
        }
    }
}
