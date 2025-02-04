using IntoTheWilds.Inventory;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace IntoTheWilds.UI
{
    public class DragAndDropHandler : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset _draggableItemAsset;

        private VisualElement _rootVisualElement;

        private Vector2 _iconOffset;
        private PlayerInventory _playerInventory;
        private InventoryHud _inventoryHud;

        private VisualElement _inventoryGrid;
        private VisualElement _draggableArea;

        private int _oldSlotIdDraggedItem;
        private ItemSlot _draggableItem;

        private VisualElement _draggableElement;
        private VisualElement _draggableElementIcon;
        private Label _draggableElementCountLabel;

        [Inject]
        public void Constuct(PlayerInventory playerInventory)
        {
            _playerInventory = playerInventory;
        }

        private void Start()
        {
            _inventoryHud = GetComponent<InventoryHud>();

            _rootVisualElement = _inventoryHud.GetComponent<UIDocument>().rootVisualElement;

            _draggableArea = _rootVisualElement.Q<VisualElement>("DraggableArea");
            _inventoryGrid = _draggableArea.Q<VisualElement>("InventoryGrid");

            InitializeDraggableElement();

            _inventoryGrid.RegisterCallback<PointerDownEvent>(PointerDownOnSlot);
        }

        private void InitializeDraggableElement()
        {
            _draggableElement = _draggableItemAsset.CloneTree();
            _draggableElementIcon = _draggableElement.Q<VisualElement>("Icon");
            _draggableElementCountLabel = _draggableElement.Q<Label>("Counter");
        }

        private void SetEnabledDraggable(bool value)
        {
            if (value == true)
            {
                _draggableArea.RegisterCallback<PointerMoveEvent>(PointerMove);
                _draggableArea.RegisterCallback<PointerUpEvent>(PointerUp);

                _draggableElement.visible = true;
            }
            else
            {
                if (_draggableItem.IsFree() != true)
                {
                    _playerInventory.ThrowAwayItem(_draggableItem);
                }

                _draggableArea.UnregisterCallback<PointerMoveEvent>(PointerMove);
                _draggableArea.UnregisterCallback<PointerUpEvent>(PointerUp);

                _draggableElement.visible = false;
            }
        }

        private void SetDraggableElement(int ItemID, int Count)
        {
            Sprite sprite = ItemsDatabase.Instance.GetInventoryIcon(ItemID);
            _draggableElementIcon.style.backgroundImage = new StyleBackground(sprite);

            _draggableElementCountLabel.text = Count.ToString();
        }

        private void PointerDownOnSlot(PointerDownEvent pointerDownEvent)
        {
            if (TryGetSlotUnderPointer(pointerDownEvent.position, out HudInventorySlot hudSlotModel) == true)
            {
                ItemSlot slotModel = _playerInventory.Inventory.GetSlotData(hudSlotModel.ID);

                if (slotModel.ItemID != 0)
                {
                    _draggableItem = new ItemSlot(slotModel.ItemID, slotModel.Count);

                    SetDraggableElement(_draggableItem.ItemID, _draggableItem.Count);

                    _oldSlotIdDraggedItem = hudSlotModel.ID;
                    _playerInventory.RemoveSlot(hudSlotModel.ID);

                    _draggableArea.Add(_draggableElement);
                    _draggableElement.style.position = Position.Absolute;

                    _iconOffset.x = hudSlotModel.VisualElement.worldBound.width / 2;
                    _iconOffset.y = hudSlotModel.VisualElement.worldBound.height / 2;

                    _draggableElement.style.left = pointerDownEvent.position.x - _iconOffset.x;
                    _draggableElement.style.top = pointerDownEvent.position.y - _iconOffset.y;

                    SetEnabledDraggable(true);
                }
            }
        }
        private void PointerMove(PointerMoveEvent pointerDownEvent)
        {
            _draggableElement.style.left = pointerDownEvent.position.x - _iconOffset.x;
            _draggableElement.style.top = pointerDownEvent.position.y - _iconOffset.y;
        }
        private void PointerUp(PointerUpEvent pointerDownEvent)
        {
            Rect inventoryGridBounds = _inventoryGrid.worldBound;

            if (inventoryGridBounds.Contains(pointerDownEvent.position))
            {
                if (TryGetSlotUnderPointer(pointerDownEvent.position, out HudInventorySlot newSlot) == true)
                {
                    ItemSlot newSlotModel = _playerInventory.Inventory.GetSlotData(newSlot.ID);

                    if (newSlotModel.ItemID == 0)
                    {
                        _playerInventory.Inventory.AddSlot(newSlot.ID, _draggableItem);

                        _draggableItem.Clear();
                    }
                    else
                    {
                        ItemSlot oldSlotModel = _playerInventory.Inventory.GetSlotData(_oldSlotIdDraggedItem);

                        if (oldSlotModel.ItemID == 0)
                        {
                            _playerInventory.Inventory.AddSlot(_oldSlotIdDraggedItem, newSlotModel);

                            _playerInventory.Inventory.AddSlot(newSlot.ID, _draggableItem);

                            _draggableItem.Clear();
                        }
                    }
                }
            }

            SetEnabledDraggable(false);
        }

        private bool TryGetSlotUnderPointer(Vector2 pointerPosition, out HudInventorySlot HudSlot)
        {
            foreach (HudInventorySlot slot in _inventoryHud.GetHudInventorySlots())
            {
                if (slot.VisualElement.worldBound.Contains(pointerPosition))
                {
                    HudSlot = slot;

                    return true;
                }
            }

            HudSlot = new HudInventorySlot
            {
                ID = -1,
                VisualElement = null,
                CountLabel = null,
            };

            return false;
        }
    }
}
