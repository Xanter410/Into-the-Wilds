using IntoTheWilds.Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using VContainer;

namespace IntoTheWilds.UI
{
    public struct HudInventorySlot
    {
        public int ID;
        public VisualElement VisualElement;
        public Label CountLabel;
        public VisualElement Icon;
    }

    public class InventoryHud : MonoBehaviour
    {
        private PlayerInventory _playerInventory;

        private UIDocument _uidocument;
        private VisualElement _inventoryGrid;

        private readonly Dictionary<int, HudInventorySlot> _inventoryHud = new();

        [Inject]
        public void Constuct(PlayerInventory playerInventory)
        {
            _playerInventory = playerInventory;
        }

        private void OnEnable()
        {
            _uidocument = GetComponent<UIDocument>();
            _inventoryGrid = _uidocument.rootVisualElement.Q<VisualElement>("InventoryGrid");

            _playerInventory.Inventory.SlotChanged += InventorySlotChanged;

            for (int slotID = 0; slotID <= _playerInventory.Inventory.Length; slotID++)
            {
                ItemSlot slot = _playerInventory.Inventory.GetSlotData(slotID);

                _inventoryHud.Add(slotID, new HudInventorySlot
                {
                    ID = slotID,
                    VisualElement = _uidocument.rootVisualElement.Q<VisualElement>("Slot_" + slotID.ToString()),
                    CountLabel = _uidocument.rootVisualElement.Q<VisualElement>("Slot_" + slotID.ToString()).Q<Label>("ItemCounter_" + slotID.ToString() + "_1"),
                    Icon = _uidocument.rootVisualElement.Q<VisualElement>("Slot_" + slotID.ToString()).Q<VisualElement>("ItemIcon_" + slotID.ToString() + "_0")
                });
            }
        }

        private void OnDisable()
        {
            _playerInventory.Inventory.SlotChanged -= InventorySlotChanged;
        }

        public bool IsUiUnderPointer()
        {
            Vector2 pointerPosition = new(
                Pointer.current.position.value.x,
                Screen.height - Pointer.current.position.value.y);

            if (_inventoryGrid.worldBound.Contains(pointerPosition))
            {
                return true;
            }

            return false;
        }

        public List<HudInventorySlot> GetHudInventorySlots()
        {
            List<HudInventorySlot> slots = new();
            foreach (var slot in _inventoryHud)
            {
                slots.Add(slot.Value);
            }

            return slots;
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
                if (slot.ItemID == 0)
                {
                    hud.CountLabel.text = "";
                    hud.Icon.style.backgroundImage = null;
                    return;
                }

                hud.CountLabel.text = slot.Count.ToString();
                var sprite = ItemsDatabase.Instance.GetInventoryIcon(slot.ItemID);
                hud.Icon.style.backgroundImage = new StyleBackground(sprite);
            }
            else
            {
                Debug.LogWarning("Error: slot не найден в словаре InventoryHUD. SlotID = " + SlotID);
            }
        }
    }
}