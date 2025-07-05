public interface IInventoryInputHandler
{
    void HandleLeftClick(ItemContainerSlotUI slot);
    void HandleRightClick(ItemContainerSlotUI slot);
    void HandleHoverEnter(ItemContainerSlotUI slot);
    void HandleHoverExit(ItemContainerSlotUI slot);
}
