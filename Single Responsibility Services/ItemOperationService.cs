public class ItemOperationService : IItemOperationService
{
    public bool TryMoveItem(ItemContainerItemUI item, ItemContainerSlotUI targetSlot)
    {
        if (targetSlot.IsEmpty)
        {
            // Clear original slot if item has one
            if (item.ItemContainerSlotUI != null)
                item.ItemContainerSlotUI.Clear();

            targetSlot.SetHeldItem(item);
            return true;
        }

        return false;
    }

    public bool TrySplitItem(ItemContainerSlotUI sourceSlot, out ItemContainerItemUI splitItem)
    {
        splitItem = null;

        if (sourceSlot.IsEmpty)
            return false;

        var sourceItem = sourceSlot.GetHeldItem();
        if (sourceItem.Amount <= 1)
            return false;

        var totalAmount = sourceItem.Amount;
        var amountToSplit = totalAmount / 2;
        var amountToRemain = totalAmount - amountToSplit;

        // Create split item
        splitItem = sourceItem.Clone();
        splitItem.Amount = amountToSplit;

        // Update source
        sourceItem.Amount = amountToRemain;
        sourceSlot.Refresh();

        return true;
    }

    public bool TryMergeItems(ItemContainerItemUI sourceItem, ItemContainerSlotUI targetSlot)
    {
        if (targetSlot.IsEmpty)
            return false;

        if (targetSlot.TryToMerge(sourceItem))
        {
            // Clear original slot if source item has one
            if (sourceItem.ItemContainerSlotUI != null)
                sourceItem.ItemContainerSlotUI.Clear();
            return true;
        }

        return false;
    }

    public bool TrySwapItems(ItemContainerItemUI item1, ItemContainerSlotUI slot2, out ItemContainerItemUI swappedItem)
    {
        swappedItem = slot2.GetHeldItem();

        // Remove item2 from its slot
        swappedItem.ItemContainerSlotUI = null;

        // Place item1 in slot2
        slot2.SetHeldItem(item1);

        return true;
    }
}
