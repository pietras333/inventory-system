using UnityEngine;

public interface IDragVisualizer
{
    void ShowDraggedItem(ItemContainerItemUI item, Vector3 position);
    void HideDraggedItem();
    void UpdatePosition(Vector3 position);
}
