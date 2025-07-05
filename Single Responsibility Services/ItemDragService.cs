public class ItemDragService : IItemDragService
{
    private ItemContainerItemUI draggedItem;

    public bool IsDragging => draggedItem != null;

    public void StartDrag(ItemContainerItemUI item)
    {
        draggedItem = item;
    }

    public void EndDrag()
    {
        draggedItem = null;
    }

    public ItemContainerItemUI GetDraggedItem()
    {
        return draggedItem;
    }
}
