using UnityEngine;

public class ItemDragManager : MonoBehaviour
{
    public static ItemDragManager Instance;

    [Header("Components")] [SerializeField]
    private InventoryInputHandler inputHandler;

    [SerializeField] private DragVisualizer dragVisualizer;

    private IItemDragService dragService;
    private IItemOperationService operationService;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeServices();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeServices()
    {
        dragService = new ItemDragService();
        operationService = new ItemOperationService();

        inputHandler.Initialize(dragService, operationService, dragVisualizer);
    }

    // Public API for external access
    public void HandleLeftClick(ItemContainerSlotUI slot)
    {
        inputHandler.HandleLeftClick(slot);
    }

    public void HandleRightClick(ItemContainerSlotUI slot)
    {
        inputHandler.HandleRightClick(slot);
    }

    public void HandleHoverEnter(ItemContainerSlotUI slot)
    {
        inputHandler.HandleHoverEnter(slot);
    }

    public void HandleHoverExit(ItemContainerSlotUI slot)
    {
        inputHandler.HandleHoverExit(slot);
    }
}
