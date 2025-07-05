public interface IItemOperationService
{
    bool TryMoveItem(ItemContainerItemUI item, ItemContainerSlotUI targetSlot);
    bool TrySplitItem(ItemContainerSlotUI sourceSlot, out ItemContainerItemUI splitItem);
    bool TryMergeItems(ItemContainerItemUI sourceItem, ItemContainerSlotUI targetSlot);
    bool TrySwapItems(ItemContainerItemUI item1, ItemContainerSlotUI slot2, out ItemContainerItemUI item2);
}
