using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DragVisualizer : MonoBehaviour, IDragVisualizer
{
    [Header("Drag Visual Components")] [SerializeField]
    private Image draggedItemRenderer;

    [SerializeField] private TextMeshProUGUI draggedItemAmountText;
    [SerializeField] private GameObject draggedItemContainer;

    public void ShowDraggedItem(ItemContainerItemUI item, Vector3 position)
    {
        draggedItemContainer.SetActive(true);
        draggedItemRenderer.sprite = item.ItemDefinition.Icon;
        draggedItemAmountText.text = item.Amount.ToString();
        draggedItemContainer.transform.position = position;
    }

    public void HideDraggedItem()
    {
        draggedItemContainer.SetActive(false);
    }

    public void UpdatePosition(Vector3 position)
    {
        if (draggedItemContainer.activeInHierarchy)
            draggedItemContainer.transform.position = position;
    }
}
