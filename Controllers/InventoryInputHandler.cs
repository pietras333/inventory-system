using UnityEngine;

public class InventoryInputHandler : MonoBehaviour, IInventoryInputHandler
{
    [Header("Services")] [SerializeField] private DragVisualizer dragVisualizer;

    private IItemDragService dragService;
    private IItemOperationService operationService;
    private IDragVisualizer visualizer;

    // Dependency Injection
    public void Initialize(IItemDragService dragService, IItemOperationService operationService,
        IDragVisualizer visualizer)
    {
        this.dragService = dragService;
        this.operationService = operationService;
        this.visualizer = visualizer;
    }

    private void Awake()
    {
        // Default initialization if not injected
        if (dragService == null)
        {
            dragService = new ItemDragService();
            operationService = new ItemOperationService();
            visualizer = dragVisualizer;
        }
    }

    public void HandleLeftClick(ItemContainerSlotUI slot)
    {
        if (dragService.IsDragging)
            HandleDropItem(slot);
        else
            HandlePickupItem(slot);
    }

    public void HandleRightClick(ItemContainerSlotUI slot)
    {
        if (dragService.IsDragging)
            HandleDropSingleItem(slot);
        else
            HandleSplitItem(slot);
    }

    public void HandleHoverEnter(ItemContainerSlotUI slot)
    {
        if (slot.IsEmpty) return;

        var item = slot.GetHeldItem();
        TooltipUI.Instance.SetItemName(item.ItemDefinition.ItemName);
        TooltipUI.Instance.SetItemDescription(item.ItemDefinition.ItemDescription);
        TooltipUI.Instance.SetTooltipActivity(true);
    }

    public void HandleHoverExit(ItemContainerSlotUI slot)
    {
        TooltipUI.Instance.SetTooltipActivity(false);
    }

    private void Update()
    {
        if (dragService.IsDragging)
            visualizer.UpdatePosition(Input.mousePosition);
        else
            visualizer.HideDraggedItem();
    }

    // ====================
    // PRIVATE OPERATION METHODS
    // ====================

    private void HandlePickupItem(ItemContainerSlotUI slot)
    {
        if (slot.IsEmpty) return;

        var item = slot.GetHeldItem();
        slot.Clear();

        dragService.StartDrag(item);
        visualizer.ShowDraggedItem(item, Input.mousePosition);

        Debug.Log($"Picked up item: {item.ItemDefinition.ItemName}");
    }

    private void HandleDropItem(ItemContainerSlotUI slot)
    {
        var draggedItem = dragService.GetDraggedItem();

        // Same slot - return item
        if (draggedItem.ItemContainerSlotUI == slot)
        {
            if (slot.IsEmpty) slot.SetHeldItem(draggedItem);
            EndDragOperation();
            return;
        }

        // Try to move to empty slot
        if (operationService.TryMoveItem(draggedItem, slot))
        {
            Debug.Log($"Moved item: {draggedItem.ItemDefinition.ItemName}");
            EndDragOperation();
            return;
        }

        // Try to merge
        if (operationService.TryMergeItems(draggedItem, slot))
        {
            Debug.Log($"Merged items: {draggedItem.ItemDefinition.ItemName}");
            EndDragOperation();
            return;
        }

        // Swap items
        if (operationService.TrySwapItems(draggedItem, slot, out var swappedItem))
        {
            // Start dragging the item that was in the slot
            dragService.StartDrag(swappedItem);
            visualizer.ShowDraggedItem(swappedItem, Input.mousePosition);
            Debug.Log(
                $"Swapped items: {draggedItem.ItemDefinition.ItemName} with {swappedItem.ItemDefinition.ItemName}");
            return;
        }
    }

    private void HandleDropSingleItem(ItemContainerSlotUI slot)
    {
        var draggedItem = dragService.GetDraggedItem();
        var singleItem = draggedItem.Clone();
        singleItem.Amount = 1;

        // Try to place single item
        if (slot.IsEmpty && operationService.TryMoveItem(singleItem, slot))
        {
            draggedItem.Amount--;
            UpdateDraggedItemDisplay();

            if (draggedItem.Amount <= 0) EndDragOperation();
            return;
        }

        // Try to merge single item
        if (operationService.TryMergeItems(singleItem, slot))
        {
            draggedItem.Amount--;
            UpdateDraggedItemDisplay();

            if (draggedItem.Amount <= 0) EndDragOperation();
        }
    }

    private void HandleSplitItem(ItemContainerSlotUI slot)
    {
        if (slot.IsEmpty) return;

        var slotItem = slot.GetHeldItem();
        if (slotItem.Amount <= 1)
        {
            HandleLeftClick(slot);
            return;
        }

        if (operationService.TrySplitItem(slot, out var splitItem))
        {
            dragService.StartDrag(splitItem);
            visualizer.ShowDraggedItem(splitItem, Input.mousePosition);
            Debug.Log($"Split item: {splitItem.ItemDefinition.ItemName}");
        }
    }

    private void UpdateDraggedItemDisplay()
    {
        var draggedItem = dragService.GetDraggedItem();
        if (draggedItem != null) visualizer.ShowDraggedItem(draggedItem, Input.mousePosition);
    }

    private void EndDragOperation()
    {
        dragService.EndDrag();
        visualizer.HideDraggedItem();
    }
}
