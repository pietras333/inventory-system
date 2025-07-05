public interface IItemDragService
{
    void StartDrag(ItemContainerItemUI item);
    void EndDrag();
    bool IsDragging { get; }
    ItemContainerItemUI GetDraggedItem();
}
